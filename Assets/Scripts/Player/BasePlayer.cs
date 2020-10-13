﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOB.Player.Movement;

namespace WOB.Player
{
    public class BasePlayer : MonoBehaviour
    {
        #region Serialized Fields
        public IPlayerMovement movement;
        public PlayerInput input;
        #endregion

        #region Monobehaviour
        // Start is called before the first frame update
        void Start()
        {
            movement = GetComponent<Walking>();
            // TODO: Move player to checkpoint location
        }

        private void FixedUpdate()
        {
            movement.Move(input.ReadInput());
        }
        #endregion

        #region Switch Movement Types
        public void SwingOnHookshot()
        {
            movement = GetComponent<HookshotSwing>();
        }

        public void StartWalking()
        {
            movement = GetComponent<Walking>();
        }

        public void StartSwimming()
        {
            movement = GetComponent<Swimming>();
        }
        #endregion
    }
}
/* The main script that attaches to the player object. 
 * Holds and acts as a bus for all player related scripts. */