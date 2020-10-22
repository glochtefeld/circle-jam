// #define DIAGNOSTIC_MODE
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    #region Serialized Fields
#pragma warning disable CS0649
    public GameObject pauseMenu;
    [Header("Buttons")]
    public Button resume;
    public Button quitToMenu;
    public Button options;
    public GameObject optionsPanel;
#pragma warning restore CS0649
    #endregion

    public static bool PauseGame { private set; get; } = true;
    #region Monobehaviour
    private void Start()
    {
        resume.onClick.AddListener(() => Resume());
        quitToMenu.onClick.AddListener(() =>
        SceneManager.LoadScene(0));
        options.onClick.AddListener(() =>
        optionsPanel.SetActive(!optionsPanel.activeSelf));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
    }
    #endregion

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
