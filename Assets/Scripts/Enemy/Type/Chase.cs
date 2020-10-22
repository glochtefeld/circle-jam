//#define DIAGNOSTIC_MODE
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
        public SpriteRenderer sprite;
        public float speed;
        [Range(0, 1)]
        public float movementSmoothing;
        #endregion

        private int _direction;
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
                _isChasing = true;
                player = collision.gameObject;
                _direction = player.transform.position.x > transform.position.x
                ? 1
                : -1;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == gameObject
                || collision.gameObject.layer == 1 << 8)
                return;
            // If Player, kill player
            if (collision.gameObject.tag == "Player")
                collision.gameObject.GetComponent<BasePlayer>().Kill();
            // else if wall, knock out
            else if (collision.gameObject.layer == 13)
                StartCoroutine(Knockout());
        }
        #endregion

        public void Move()
        {
            if (!_isChasing)
                return;
            
            rigidbody.velocity = Vector3.SmoothDamp(
                rigidbody.velocity,
                new Vector2(_direction * speed * 50 * Time.fixedDeltaTime,
                    rigidbody.velocity.y),
                ref _velocity,
                movementSmoothing);
            sprite.flipX = _direction < 0;  
        }

        private IEnumerator Knockout()
        {
            Debug.Log($"Collided With Wall");
            _isChasing = false;
            _checkForPlayer = false;
            yield return new WaitForSeconds(10f);
            _checkForPlayer = true;
        }
#if DIAGNOSTIC_MODE
        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 100, 25), $"{_direction}");
        }
#endif
    }
}