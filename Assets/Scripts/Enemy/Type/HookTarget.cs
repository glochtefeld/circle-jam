using System;
using UnityEngine;

namespace WOB.Enemy.Type
{
    public class HookTarget : MonoBehaviour, IEnemy
    {
        private bool _moving = false;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            /* Check to make sure the item entering the trigger is
             * the hookshot itself. */
            throw new NotImplementedException();
        }

        public void Move()
        {
            if (!_moving)
                return;
            /* Fly up 10 units, then slowly drift back down to the starting
             * position. */
            throw new NotImplementedException();
        }
    }
}