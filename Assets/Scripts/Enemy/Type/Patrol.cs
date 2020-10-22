using System;
using UnityEngine;

namespace WOB.Enemy.Type
{
    public class Patrol : MonoBehaviour, IEnemy
    {
        public Transform[] positions;
        public float lerpTime;

        private int _target;

        private float _time;
        public void Move()
        {
            if (AtTarget())
            {
                _target++;
                _target = _target >= positions.Length ? 0 : _target;
                _time = 0f;
            }
            var ratio = _time / lerpTime;
            transform.position = Vector2.Lerp(
                transform.position,
                positions[_target].position,
                ratio);        
            _time += Time.fixedDeltaTime;

        }

        private bool AtTarget() =>
            Vector2.Distance(transform.position,
                positions[_target].position) < 0.05f;
    }
}