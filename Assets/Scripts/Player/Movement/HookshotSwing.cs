using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WOB.Player.Movement
{
    public class HookshotSwing : MonoBehaviour, IPlayerMovement
    {

        #region Serialized Fields
        public float climbSpeed;
        public SetHook hookController;
        public float swingSpeed;
        [Range(0, 1)]
        public float movementSmoothing;
        public new Rigidbody2D rigidbody;
        [Header("Audio")]
        public AudioControl audioControl;

        public UnityEvent onSwingEvent;
        #endregion

        private void Start()
        {
            if (onSwingEvent == null)
                onSwingEvent = new UnityEvent();
        }

        private const int PIXEL_SIZE = 50;
        private Vector3 _velocity;
        private bool _playingSound;
        #region IPlayerMovement
        public void Move(Vector2 direction)
        {
            // Up goes toward hook, down goes away
            hookController.SetDistance(
                direction.y * climbSpeed * -1);
            var move = direction.x
                * PIXEL_SIZE
                * swingSpeed
                * Time.fixedDeltaTime;
            var targetVelocity = new Vector2(
                move,
                rigidbody.velocity.y);
            rigidbody.velocity = Vector3.SmoothDamp(
                rigidbody.velocity,
                targetVelocity,
                ref _velocity,
                movementSmoothing);
            if (direction.x != 0 && !_playingSound)
            {
                StartCoroutine(PlaySwing());
                _playingSound = true;
            }

        }
        #endregion

        private IEnumerator PlaySwing()
        {
            if (!_playingSound)
            {
                audioControl.PlaySFX(SFX.Swing);
                yield return new WaitForSeconds(0.75f);
                _playingSound = false;
            }

        }
    }
}