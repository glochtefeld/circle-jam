using UnityEngine;
using UnityEngine.Events;

namespace WOB.Player.Movement
{
    public class Walking : MonoBehaviour, IPlayerMovement
    {
        #region Serialized Fields
        [Header("Movement")]
        public float speed;
        public float jumpVelocity;
        public bool airControl;
        [Range(0, 1)]
        public float crouchSpeedMultiplier;
        public new Rigidbody2D rigidbody;
        [Range(0, 1)]
        [Tooltip("The ramp up time for getting to speed")]
        public float movementSmoothing;
        [Range(0, 1)]
        [Tooltip("The ramp up time for getting to speed")]
        public float movementSmoothingDown;
        [Header("Jumping Help")]
        [Tooltip("How long the player has to press \"Jump\" " +
            "before hitting the ground do we let them jump")]
        public float maxJumpPressedMemory;
        [Tooltip("How long the player can hang in the air after running off" +
            "a platform before they can no longer jump")]
        public float maxGroundedMemory;
        [Header("Collision Bounding")]
        public LayerMask ground;
        [Tooltip("Transform at the bottom of the player")]
        public Transform groundCheck;
        [Tooltip("Transform at the top of the player")]
        public Transform ceilingCheck;
        [Tooltip("Collider to disable when crouching")]
        public Collider2D crouchDisableCollider;
        [Space]
        [Header("Events")]
        public UnityEvent onLandEvent;
        public BoolEvent onCrouchEvent;

        #endregion

        #region Monobehaviour
        private void Awake()
        {
            if (onLandEvent == null)
                onLandEvent = new UnityEvent();
            if (onCrouchEvent == null)
                onCrouchEvent = new BoolEvent();
        }
        #endregion

        private Vector3 _velocity;
        private bool _facingRight = true;
        private bool _jumping;
        private bool _isGrounded;
        private bool _isCrouching;
        private const float GROUND_CHECK_RADIUS = 0.2f;
        private const float CEIL_CHECK_RADIUS = 0.2f;
        private float _jumpPressedTime = 0f;
        private float _groundedMemoryTime = 0f;

        #region IPlayerMovement
        public void Move(Vector2 direction)
        {
            var crouch = direction.y < 0;
            var move = direction.x * speed * Time.fixedDeltaTime;
            // Initial Grounded Check
            IsGrounded();

            if (direction.y > 0)
                _jumpPressedTime = maxJumpPressedMemory;

            // Force crouch if under a ceiling
            var ceilingCollider = Physics2D.OverlapCircle(
                ceilingCheck.position,
                CEIL_CHECK_RADIUS,
                ground);
            if (!crouch && ceilingCollider != null)
                crouch = true;

            // Reduce speed when crouching and move L/R
            if (_isGrounded || airControl)
            {
                if (crouch)
                {
                    if (!_isCrouching)
                    {
                        _isCrouching = true;
                        onCrouchEvent.Invoke(true);
                    }

                    move *= crouchSpeedMultiplier;

                    if (crouchDisableCollider != null)
                        crouchDisableCollider.enabled = false;
                }
                else
                {
                    if (crouchDisableCollider != null)
                        crouchDisableCollider.enabled = true;
                    if (_isCrouching)
                    {
                        _isCrouching = false;
                        onCrouchEvent.Invoke(false);
                    }
                }
                
                // move left / right
                Vector3 targetVelocity = new Vector2(
                    move,
                    rigidbody.velocity.y);
                if (move == 0)
                    rigidbody.velocity = Vector3.SmoothDamp(
                        rigidbody.velocity,
                        targetVelocity,
                        ref _velocity,
                        movementSmoothingDown);
                else
                    rigidbody.velocity = Vector3.SmoothDamp(
                        rigidbody.velocity,
                        targetVelocity,
                        ref _velocity,
                        movementSmoothing);

                // Flip sprite
                if ((move > 0 && !_facingRight)
                    || (move < 0 && _facingRight))
                    Flip();
            }

            // Jumping
            if (_groundedMemoryTime > 0
                && _jumpPressedTime > 0
                && rigidbody.velocity.y < 10f)
            {
                _isGrounded = false;
                rigidbody.velocity = new Vector2(
                    rigidbody.velocity.x,
                    jumpVelocity);
            }
        }
        #endregion
        private void Flip()
        {
            _facingRight = !_facingRight;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        private void IsGrounded()
        {
            var wasGrounded = _isGrounded;
            _isGrounded = false;
            _groundedMemoryTime -= Time.fixedDeltaTime;
            _jumpPressedTime -= Time.fixedDeltaTime;
            var groundColliders = Physics2D.OverlapCircleAll(
                groundCheck.position,
                GROUND_CHECK_RADIUS,
                ground);
            foreach (var coll in groundColliders)
            {
                _isGrounded = true;
                _groundedMemoryTime = maxGroundedMemory;
                if (!wasGrounded)
                    onLandEvent.Invoke();
            }
        }
    }

    public class BoolEvent : UnityEvent<bool> { }
}
/* The default locomotion for the player. By far the most complicated
 * of all the movement types, but it sees the most use. */