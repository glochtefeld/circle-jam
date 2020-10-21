using System;
using System.Collections;
using UnityEngine;
using WOB.Player;

namespace WOB.Enemy.Type
{
    public class Chase : MonoBehaviour, IEnemy
    {
        #region Serialized Fields
        public new Rigidbody2D rigidbody;
        public float speed;
        [Range(0, 1)]
        public float movementSmoothing;
        #endregion

        private bool _checkForPlayer = true;
        private bool _isChasing = false;
        private Vector3 _velocity;
        private GameObject player;
        
        
        #region Monobehaviour
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_checkForPlayer || collision.gameObject == gameObject)
                return;
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log($"Triggered Chase");
                _isChasing = true;
                player = collision.gameObject;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == gameObject)
                return;
            Debug.Log($"Collided");
            // If Player, kill player
            if (collision.gameObject.tag == "Player")
                collision.gameObject.GetComponent<BasePlayer>().Kill();
            // else if wall, knock out
            else if (collision.gameObject.layer == 1 << 8)
                StartCoroutine(Knockout());
        }
        #endregion

        public void Move()
        {
            if (!_isChasing)
                return;
            var direction = player.transform.position.x > transform.position.x
                ? 1
                : -1;
            rigidbody.velocity = Vector3.SmoothDamp(
                rigidbody.velocity,
                new Vector2(direction * speed * Time.fixedDeltaTime,
                    rigidbody.velocity.y),
                ref _velocity,
                movementSmoothing);
            transform.localScale = new Vector3(direction, 1, 1);
            
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