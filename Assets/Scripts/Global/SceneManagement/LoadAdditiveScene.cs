// #define DIAGNOSTIC_MODE
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAdditiveScene : MonoBehaviour
{
    #region Serialized Fields
#pragma warning disable CS0649
    public string sceneToLoad;
    public string sceneToUnload;
#pragma warning restore CS0649
    #endregion

    #region Monobehaviour
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;
        if (sceneToLoad != "" 
            && !SceneManager.GetSceneByName(sceneToLoad).isLoaded)
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        if (sceneToUnload != ""
            && SceneManager.GetSceneByName(sceneToUnload).isLoaded)
            SceneManager.UnloadSceneAsync(sceneToUnload);
    }
    #endregion

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
