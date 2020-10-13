using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOB.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public BasePlayer player;

        private Vector2 _input;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _input = new Vector2(Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"));
        }

        public Vector2 ReadInput() => _input;

    }
}