using UnityEngine;

namespace WOB.Player.Movement
{
    public class Walking : MonoBehaviour, IPlayerMovement
    {
        #region Serialized Fields
        [Header("Movement")]
        public float speed;
        public new Rigidbody2D rigidbody;
        [Range(0, 1)]
        [Tooltip("The ramp up time for getting to speed")]
        public float movementSmoothing;
        [Header("Jumping Help")]
        [Tooltip("How long the player has to press \"Jump\" " +
            "before hitting the ground do we let them jump")]
        public float jumpPressedMemory;
        [Tooltip("How long the player can hang in the air after running off" +
            "a platform before they can no longer jump")]
        public float groundedMemory;
        #endregion

        private Vector3 _velocity;
        private bool _facingRight;
        private bool _jumping;

        #region IPlayerMovement
        public void Move(Vector2 direction)
        {
            // move left / right
            Vector3 targetVelocity = new Vector2(
                direction.x * speed * Time.fixedDeltaTime,
                rigidbody.velocity.y);
            rigidbody.velocity = Vector3.SmoothDamp(
                rigidbody.velocity,
                targetVelocity,
                ref _velocity,
                movementSmoothing);
            
            // Flip sprite
            if (direction.x > 0 && _facingRight
                || direction.x < 0 && !_facingRight)
                Flip();

            // Jump

            // Crouch
 
        }
        #endregion
        private void Flip()
        {
            _facingRight = !_facingRight;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}