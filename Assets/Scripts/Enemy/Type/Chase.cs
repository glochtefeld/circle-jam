using System;
using System.Collections;
using UnityEngine;

namespace WOB.Enemy.Type
{
    public class Chase : MonoBehaviour, IEnemy
    {
        private bool _checkForPlayer = true;
        private bool _isChasing = false;

        #region Monobehaviour
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_checkForPlayer)
                return;
            /* If the collision gameobject is the player, set _isChasing
             * to true. */
            throw new NotImplementedException();
        }

        #endregion

        public void Move()
        {
            if (!_isChasing)
                return;
            /* Move in a straight line towards the player. If we 
             * collide with a wall, call the Knockout Coroutine. */
            throw new NotImplementedException();
        }

        private IEnumerator Knockout()
        {
            _isChasing = false;
            _checkForPlayer = false;
            yield return new WaitForSeconds(10f);
            _checkForPlayer = true;
        }
    }
}