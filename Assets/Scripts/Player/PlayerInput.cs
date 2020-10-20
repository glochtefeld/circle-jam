using UnityEngine;

namespace WOB.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector2 _input;
        public Vector2 ReadInput() => _input;

        void Update()
        {
            _input = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"));
        }
    }
}
/* Reads keyboard input and stores it to be read later. */