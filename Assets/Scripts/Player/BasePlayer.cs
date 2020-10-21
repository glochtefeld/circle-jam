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
        #endregion
        
        private IPlayerMovement movement;
        private bool _hookWasShot;
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
        }
        #endregion

        public void Kill()
        {
            // TODO: Implement scene reloading & checkpoints
        }

        #region Switch Movement Types
        public void SwingOnHookshot() => movement = mover.GetComponent<HookshotSwing>();
        public void StartWalking() => movement = mover.GetComponent<Walking>();
        public void StartSwimming() => movement = mover.GetComponent<Swimming>();
        public void StartClimbing() => movement = mover.GetComponent<Climbing>();
        #endregion

        private void ShootHook()
        {
            StartCoroutine(Hook());
        }

        private IEnumerator Hook()
        {
            // Prevent hookshot from being fired before it retracts for 
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
            // after x time, retract hook quickly
            yield return new WaitForSeconds(maxHookTime);
            Destroy(hook);
            _hookWasShot = false;
        }
    }
}
/* The main script that attaches to the player object. 
 * Holds and acts as a bus for all player related scripts. */