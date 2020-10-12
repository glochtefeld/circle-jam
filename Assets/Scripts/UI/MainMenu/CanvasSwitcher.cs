using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOB.UI
{
    public class CanvasSwitcher : MonoBehaviour
    {
        #region Serialized Fields
#pragma warning disable CS0649

#pragma warning restore CS0649
        #endregion

        private List<Canvas> _canvasOptions = new List<Canvas>();
        private Canvas _activeCanvas;
        #region Monobehaviour
        private void Awake()
        {
            // See comment below
            for (int i = 1; i < transform.childCount; i++)
            {
                _canvasOptions.Add(
                    transform.GetChild(i).GetComponent<Canvas>());
            }
            _activeCanvas = _canvasOptions[0];
        }
        #endregion

        public void SwitchCanvas(Canvas to)
        {
            _activeCanvas.gameObject.SetActive(false);
            to.gameObject.SetActive(true);
            _activeCanvas = to;
        }

        public void SwitchCanvasFade(Canvas to,float fade, bool fadeOut=false)
        {
            _activeCanvas.gameObject.SetActive(false);
            to.gameObject.SetActive(true);
            if (fadeOut)
                StartCoroutine(FadeCanvas(
                    _activeCanvas.GetComponent<CanvasGroup>(),
                    fade,
                    false));
            StartCoroutine(FadeCanvas(to.GetComponent<CanvasGroup>(),
                fade,
                true));
        }

        public void DeactivateCurrentCanvas() => 
            _activeCanvas.gameObject.SetActive(false);

        public Canvas this[int i]
        {
            get { return _canvasOptions[i]; }
        }

        private IEnumerator FadeCanvas(
            CanvasGroup cg, 
            float fadeRate, 
            bool direction)
        {
            var target = direction ? 1f : 0f;
            if (direction)
                while (cg.alpha < target)
                {
                    cg.alpha += fadeRate;
                    yield return null;
                }
            else 
                while (cg.alpha > target)
                {
                    cg.alpha -= fadeRate;
                    yield return null;
                }
            cg.alpha = target;
        }
    }
}
/* A utility to easily switch between canvases.
 * Note that the first canvas, Static, isn't added when building the 
 * list of canvases. */