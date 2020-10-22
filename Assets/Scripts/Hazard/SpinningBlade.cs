using System;
using UnityEngine;
using WOB.Player;

namespace WOB.Hazard
{
    public class SpinningBlade : MonoBehaviour
    {
        public float rotationSpeed;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var potentialPlayer = collision.gameObject
                .GetComponent<BasePlayer>();
            if (potentialPlayer != null)
                potentialPlayer.Kill();
        }

        private void FixedUpdate()
        {
            transform.Rotate(0f, 0f, rotationSpeed,Space.Self);
        }
    }
}