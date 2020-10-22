#define DIAGNOSTIC
using System;
using System.Collections;
using UnityEngine;

namespace WOB.Enemy.Type
{
    public class HookTarget : MonoBehaviour, IEnemy
    {
        public Transform hookPoint;
        public new Rigidbody2D rigidbody;
        public float riseDistance;
        public float riseLerpTime;
        public float fallLerpTime;
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
            if (Moving && !_moveLock)
            {
                Moving = false;
                _moveLock = true;
                StartCoroutine(FlyUpAndDown());
            }
            
        }

        private IEnumerator FlyUpAndDown()
        {
            // Shoot up quickly
            var targetPos = new Vector2(
                transform.position.x,
                transform.position.y + riseDistance);
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

            // Wait a second
            yield return new WaitForSeconds(1f);
            
            // Float back down slowly
            time = 0f;
            ratio = 0f;
            while (time < fallLerpTime
                && Vector2.Distance(rigidbody.position,_startPos) > 0.05f)
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

#if DIAGNOSTIC
        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 150, 25), $"Ratio: {ratio}");
        }
#endif
    }
}
/* Is using an internal state consistent with any other class? no.
 * Are we at the point where it's time to get this finished? yes. */