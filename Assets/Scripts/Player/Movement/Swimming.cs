using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOB.Player.Movement
{
    public class Swimming : MonoBehaviour, IPlayerMovement
    {
        #region Serialized Fields
        [Header("RigidBody")]
        [Tooltip("assign the rigidbody of the object")]
        public Rigidbody2D rb;

        [Header("speed")]
        [Tooltip("adjust the movement smoothing")]
        [Range(0f, 1f)]
        public float movementSmoothing;

        [Header("Horizontal Speed")]
        public float horizontalSpeed;

        [Header("Vertical Speed")]
        public float verticalSpeed;
        #endregion
        private const int _pixelSize = 50;
        private Vector3 _velocity;

        #region Monobehaviour
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region IPlayerMovement
        public void Move(Vector2 direction)
        {
            var target = new Vector2();
            target.x = direction.x
                * horizontalSpeed
                * _pixelSize
                * Time.fixedDeltaTime;
            target.y = direction.y
                * verticalSpeed
                * _pixelSize
                * Time.fixedDeltaTime;

            rb.velocity = Vector3.SmoothDamp(
                rb.velocity,
                target,
                ref _velocity,
                movementSmoothing);
        }
        #endregion
    }
}