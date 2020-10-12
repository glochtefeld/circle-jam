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
            _start.onClick.AddListener(() => SceneManager.LoadScene(1));
            _toOptions.onClick.AddListener(() => _switcher.SwitchCanvas(_switcher[1]));
            _toCredits.onClick.AddListener(() => _switcher.SwitchCanvas(_switcher[2]));
            _fromOptionsToStart.onClick.AddListener(() => _switcher.SwitchCanvas(_switcher[0]));
            _fromCreditsToStart.onClick.AddListener(() => _switcher.SwitchCanvas(_switcher[0]));
            _quit.onClick.AddListener(() => Application.Quit());
        }
        #endregion
    }
}