using UnityEngine;

namespace WOB.Player.Movement
{
    public class Climbing : MonoBehaviour, IPlayerMovement
    {
        public new Rigidbody2D rigidbody;
        [Range(0, 1)]
        public float movementSmoothing;
        [Header("Horizontal Values")]
        public float horzSpeed;
        [Header("Vertical Values")]
        public float vertSpeed;

        private const int PIXEL_SIZE = 50;
        private Vector3 _velocity;
        
        public void Move(Vector2 direction)
        {
            var target = new Vector2();
            target.x = direction.x
                * horzSpeed 
                * PIXEL_SIZE 
                * Time.fixedDeltaTime;
            target.y = direction.y
                * vertSpeed
                * PIXEL_SIZE
                * Time.fixedDeltaTime;

            rigidbody.velocity = Vector3.SmoothDamp(
                rigidbody.velocity,
                target,
                ref _velocity,
                movementSmoothing);
        }
    }
}