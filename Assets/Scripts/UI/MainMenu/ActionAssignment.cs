using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WOB.UI
{
    public class ActionAssignment : MonoBehaviour
    {
        #region Serialized Fields
#pragma warning disable CS0649
        public CanvasSwitcher _switcher;
        [Header("Main Buttons")]
        public Button _start;
        public Button _toOptions;
        public Button _toCredits;
        public Button _quit;
        [Header("Back to Main buttons")]
        public Button _fromOptionsToStart;
        public Button _fromCreditsToStart;
#pragma warning restore CS0649
        #endregion

        #region Monobehaviour
        void Start()
        {
            // Buttons
            _start.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Player");
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
                SceneManager.LoadScene(2, LoadSceneMode.Additive);
            });
            _toOptions.onClick.AddListener(() =>
            {
                _toOptions.interactable = false;
                _switcher.TweenCanvasSwitch(_switcher[1]);
                _toCredits.interactable = true;
            });
            _toCredits.onClick.AddListener(() =>
            {
                _toCredits.interactable = false;
                _switcher.TweenCanvasSwitch(_switcher[2]);
                _toOptions.interactable = true;
            });
            //_fromOptionsToStart.onClick.AddListener(() => _switcher.SwitchCanvas(_switcher[0]));
            //_fromCreditsToStart.onClick.AddListener(() => _switcher.SwitchCanvas(_switcher[0]));
            _quit.onClick.AddListener(() => Application.Quit());
        }
        #endregion
    }
}