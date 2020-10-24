// #define DEBUG_MODE
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using WOB.Player.Movement;

namespace WOB.Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(IPlayerMovement))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class BasePlayer : MonoBehaviour
    {
        #region Serialized Fields
        public PlayerInput input;
        public GameObject mover;
        [Header("Hookshot")]
        public float hookForce;
        public float cooldownTime;
        public float speed;
        public float maxHookTime;
        public GameObject hookObject;
        public float returnLerpTime;
        public GameObject hookController;
        public LineRenderer line;
        [Header("Death")]
        public CanvasGroup deathPanel;
        public float fadeRate;
        [Header("Audio")]
        public AudioControl audioControl;
        [Header("Animation")]
        public Animator anim;
        #endregion
        
        private IPlayerMovement movement;
        private bool _hookWasShot;
        private bool _hooked;
        private bool _dead;
        public int Score { set; get; }

        private float ratio;
        #region Monobehaviour
        // Start is called before the first frame update
        void Start()
        {
            _hookWasShot = false;
            movement = mover.GetComponent<Walking>();
            // TODO: Move player to checkpoint location
            if (CheckpointManager.Instance != null)
            {
                transform.position = CheckpointManager.Instance.StartPosition;
            }
        }

        private void FixedUpdate()
        {
            movement.Move(input.ReadInput());
            if (input.Mouse() && !_hookWasShot)
                ShootHook();
            else if (!input.Mouse() && _hooked)
            {
                _hooked = false;
                _hookWasShot = false;
                hookController.GetComponent<SetHook>()
                    .UnHook();

                StartWalking();
            }
        }
        #endregion

        public void Kill()
        {
            audioControl.PlaySFX(SFX.Death);
            var ps = GameObject.Find("/DeathLaser");
            if (ps != null)
                ps.GetComponent<ParticleSystem>().Stop();
            _dead = true;
            deathPanel.gameObject.SetActive(true);
            Debug.Log($"Killed Player");
            // Activate Death screen
            StartCoroutine(Death());
            // get all scenes that aren't the player scene
            var loadedNum = SceneManager.sceneCount;
            var loadedScenes = new Scene[loadedNum];
            for (int i = 0; i < loadedNum; i++)
                loadedScenes[i] = SceneManager.GetSceneAt(i);
            var startPos = CheckpointManager.Instance.StartPosition;
            StartCoroutine(UnloadScenes(loadedScenes,startPos));

        }

        public void AddPoints() => Score++;

        #region Switch Movement Types
        public void SwingOnHookshot() 
        {
            anim.SetBool("isSwimming", false);
            anim.SetBool("isSwinging", true);
            movement = mover.GetComponent<HookshotSwing>();
            GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
        
        public void StartWalking()
        {
            anim.SetBool("isSwimming", false);
            anim.SetBool("isSwinging", false);
            movement = mover.GetComponent<Walking>();
            GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
        
        public void StartSwimming() 
        {
            anim.SetBool("isSwinging", false);
            anim.SetBool("isSwimming", true);
            audioControl.PlaySFX(SFX.Splash);
            movement = mover.GetComponent<Swimming>();
            GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        }
        
        public void StartClimbing()
        {
            anim.SetBool("isSwimming", false);
            anim.SetBool("isSwinging", false);
            movement = mover.GetComponent<Climbing>();
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        } 
        #endregion

        private void ShootHook()
        {
            StartCoroutine(Hook());
        }

        private IEnumerator Hook()
        {
            audioControl.PlaySFX(SFX.HookFire);
            // Prevent hookshot from being fired
            _hookWasShot = true;
            // Instantiate hook
            var hook = Instantiate(
                hookObject,
                transform.position,
                RotateArmForHookshot.Angle);
            // Add force to hook instance in direction
            hook.GetComponent<Rigidbody2D>().AddForce(
                hook.transform.right * hookForce,
                ForceMode2D.Impulse);

            // Check for collision with hookable 
            float time = 0;
            var hooked = hook.GetComponent<CheckHookable>().Hooked;
            line.enabled = true;
            while ((time < maxHookTime) && !hooked && input.Mouse())
            {
                if (_dead)
                    break;
                time += Time.deltaTime;
                try
                {
                    hooked = hook.GetComponent<CheckHookable>().Hooked;
                }
                catch { }

                line.SetPositions(new Vector3[]
                {
                    transform.position,
                    hook.transform.position
                });
                yield return null;
            }

            if (hooked)
            {
                audioControl.PlaySFX(SFX.HookCollideSucceed);
                _hooked = true;
                var target = hook.GetComponent<CheckHookable>()
                    .@object;
                hookController.GetComponent<SetHook>().Hook(
                    target.GetComponent<Rigidbody2D>(),
                    Vector2.Distance(
                        target.transform.position,
                        transform.position));
                SwingOnHookshot();
            }
            else
            {
                audioControl.PlaySFX(SFX.HookCollideFail);
                hook.GetComponent<Rigidbody2D>().velocity = 
                    Vector2.zero;
                time = 0;
                ratio = 0f;
                while (
                    Vector2.Distance(
                        hook.transform.position,
                        transform.position) 
                    > 0.05f)
                {
                    time += Time.deltaTime;
                    ratio = time / returnLerpTime;
                    hook.transform.position = Vector2.Lerp(
                        hook.transform.position,
                        transform.position,
                        ratio);
                    line.SetPositions(new Vector3[]
                    {
                        transform.position,
                        hook.transform.position
                    });
                    yield return null;
                }
                _hookWasShot = false;
                line.enabled = false;
            }
            // Cleanup

            DestroyImmediate(hook);
        }

        private IEnumerator Death()
        {
            while (deathPanel.alpha < 1)
            {
                deathPanel.alpha += fadeRate;
                yield return null;
            }
            deathPanel.alpha = 1;
        }

        private IEnumerator UnloadScenes(Scene[] scenes, Vector3 startPos)
        {
            var names = new List<string>();
            foreach (var scene in scenes)
            {
                if (scene.name != "Player")
                {
                    names.Add(scene.name);
                    yield return SceneManager.UnloadSceneAsync(scene);
                }
            }
            while (!Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
            }
            names.Reverse();
            foreach (var scene in names)
            {
                if (scene != "Player")
                    SceneManager.LoadScene(scene,LoadSceneMode.Additive);
            }
            deathPanel.alpha = 0;
            transform.position = startPos;
            _dead = false;
        }

#if DEBUG_MODE
        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 100, 100), $"Scenes Loaded: {SceneManager.sceneCount}");
        }
#endif
    }
}
/* The main script that attaches to the player object. 
 * Holds and acts as a bus for all player related scripts. */