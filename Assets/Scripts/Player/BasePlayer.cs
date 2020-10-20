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
        public float cooldownTime;
        public float speed;
        public float maxHookTime;
        #endregion
        
        private IPlayerMovement movement;
        #region Monobehaviour
        // Start is called before the first frame update
        void Start()
        {
            movement = mover.GetComponent<Walking>();
            // TODO: Move player to checkpoint location
        }

        private void FixedUpdate()
        {
            movement.Move(input.ReadInput());
            if (input.Mouse())
                ShootHook();
        }
        #endregion

        public void Kill()
        {

        }

        #region Switch Movement Types
        public void SwingOnHookshot() => movement = mover.GetComponent<HookshotSwing>();
        public void StartWalking() => movement = mover.GetComponent<Walking>();
        public void StartSwimming() => movement = mover.GetComponent<Swimming>();
        public void StartClimbing() => movement = mover.GetComponent<Climbing>();
        #endregion

        private void ShootHook() => StartCoroutine(Hook());

        private IEnumerator Hook()
        {
            // Prevent hookshot from being fired before it retracts for 
            // Instantiate hook

            // Add force to hook instance in direction

            // after x time, retract hook quickly
            yield return null;
        }
    }
}
/* The main script that attaches to the player object. 
 * Holds and acts as a bus for all player related scripts. */