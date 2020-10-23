// #define DIAGNOSTIC
using System.Collections;
using UnityEngine;

namespace WOB.Enemy.Type
{
    public class HookTarget : MonoBehaviour, IEnemy
    {
        public Transform hookPoint;
        public new Rigidbody2D rigidbody;
        public Vector3 relativeTargetPosition;
        public float riseLerpTime;
        public float fallLerpTime;
        public bool fallBackDown = true;
        public bool partOfPuzzle = false;
        public bool Moving { set; get; } = false;

        private bool _moveLock = false;
        private Vector3 _startPos;

        private float ratio;
        private void Start()
        {
            _startPos = transform.position;
        }

        public void Move()
        {
            if (Moving && !_moveLock && !partOfPuzzle)
            {
                Moving = false;
                _moveLock = true;
                StartCoroutine(FlyUpAndDown());
            }
            
        }

        private IEnumerator FlyUpAndDown()
        {
            // Shoot up quickly
            var targetPos = new Vector3(
                transform.position.x + relativeTargetPosition.x,
                transform.position.y + relativeTargetPosition.y,
                transform.position.z + relativeTargetPosition.z);
            if (!fallBackDown)
                this.enabled = false;
            var time = 0f;
            while (time < riseLerpTime
                && Vector2.Distance(rigidbody.position, targetPos) > 0.05f)
            {
                time += Time.fixedDeltaTime;
                ratio = time / riseLerpTime;
                rigidbody.position = Vector2.Lerp(
                    rigidbody.position,
                    targetPos,
                    ratio);
                yield return null;
            }
            Debug.Log($"Finished Rising");
            if (fallBackDown)
            {
                // Wait a second
                yield return new WaitForSeconds(1f);

                // Float back down slowly
                time = 0f;
                ratio = 0f;
                while (time < fallLerpTime
                    && Vector2.Distance(rigidbody.position, _startPos) > 0.05f)
                {
                    time += Time.fixedDeltaTime;
                    ratio = time / fallLerpTime;
                    rigidbody.position = Vector2.Lerp(
                        rigidbody.position,
                        _startPos,
                        ratio);
                    yield return null;
                }
                _moveLock = false;
                Debug.Log($"Finished falling");
            }
            else
            {
                relativeTargetPosition = Vector3.zero;
                this.enabled = false;
            }
            
        }

#if DIAGNOSTIC
        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 150, 25), $"Pos: {rigidbody.position}");
        }
#endif
    }
}
/* Is using an internal state consistent with any other class? no.
 * Are we at the point where it's time to get this finished? yes. */