using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOB.Player.Movement
{
    public class HookshotSwing : MonoBehaviour, IPlayerMovement
    {

        #region Serialized Fields
        public float climbSpeed;
        public SetHook hookController;
        public float swingSpeed;
        [Range(0, 1)]
        public float movementSmoothing;
        public new Rigidbody2D rigidbody;
        #endregion

        private const int PIXEL_SIZE = 50;
        private Vector3 _velocity;
        #region IPlayerMovement
        public void Move(Vector2 direction)
        {
            // Up goes toward hook, down goes away
            hookController.SetDistance(
                direction.y * climbSpeed * -1);
            var move = direction.x
                * PIXEL_SIZE
                * swingSpeed
                * Time.fixedDeltaTime;
            var targetVelocity = new Vector2(
                move,
                rigidbody.velocity.y);
            rigidbody.velocity = Vector3.SmoothDamp(
                rigidbody.velocity,
                targetVelocity,
                ref _velocity,
                movementSmoothing);


        }
        #endregion

    }
}