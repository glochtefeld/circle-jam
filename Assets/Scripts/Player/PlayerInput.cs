using UnityEngine;

namespace WOB.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector2 _input;
        private bool mouseClicked;
        public Vector2 ReadInput() => _input;
        public bool Mouse() => mouseClicked;

        void Update()
        {
            mouseClicked = Input.GetKey(KeyCode.Mouse0);
            _input = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"));
        }
    }
}
/* Reads keyboard input and stores it to be read later. */