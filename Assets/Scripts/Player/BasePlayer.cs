using System.Collections;
using UnityEngine;
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
        #endregion
        
        private IPlayerMovement movement;
        private bool _hookWasShot;
        private bool _hooked;

        private float ratio;
        #region Monobehaviour
        // Start is called before the first frame update
        void Start()
        {
            _hookWasShot = false;
            movement = mover.GetComponent<Walking>();
            // TODO: Move player to checkpoint location
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
            // TODO: Implement scene reloading & checkpoints
        }

        #region Switch Movement Types
        public void SwingOnHookshot() 
        {
            movement = mover.GetComponent<HookshotSwing>();
            GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
        
        public void StartWalking()
        {
            movement = mover.GetComponent<Walking>();
            GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
        
        public void StartSwimming() 
        {
            movement = mover.GetComponent<Swimming>();
            GetComponent<Rigidbody2D>().gravityScale = 0.3f;
        }
        
        public void StartClimbing()
        {
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
                time += Time.deltaTime;
                hooked = hook.GetComponent<CheckHookable>().Hooked;

                line.SetPositions(new Vector3[]
                {
                    transform.position,
                    hook.transform.position
                });
                yield return null;
            }

            if (hooked)
            {
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

        // Debugging
        //private void OnGUI()
        //{
        //    GUI.Label(new Rect(0, 0, 100, 50), $"_hooked:{_hooked}");
        //    GUI.Label(new Rect(0, 50, 100, 50), $"_hookWasShot:{_hookWasShot}");
        //    GUI.Label(new Rect(0, 100, 100, 50), $"Mouse:{input.Mouse()}");
        //    GUI.Label(new Rect(0, 150, 100, 50), $"Ratio:{ratio}");
        //}
    }
}
/* The main script that attaches to the player object. 
 * Holds and acts as a bus for all player related scripts. */