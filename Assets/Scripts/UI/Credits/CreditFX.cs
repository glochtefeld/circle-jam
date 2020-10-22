// #define DIAGNOSTIC_MODE
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditFX : MonoBehaviour
{
    #region Serialized Fields
#pragma warning disable CS0649
    public GameObject creditsPanel;
#pragma warning restore CS0649
#endregion

#region Monobehaviour
    void Start()
    {
        StartCoroutine(ScrollupSlowly());
    }

    void Update()
    {
        
    }
#endregion

    private IEnumerator ScrollupSlowly()
    {
        yield return new WaitForSeconds(3f);
        var time = 0f;
        while (time < 60f)
        {
            time += Time.fixedDeltaTime;
            creditsPanel.transform.position =
                new Vector3(
                    creditsPanel.transform.position.x,
                    creditsPanel.transform.position.y + 0.003f,
                    creditsPanel.transform.position.z);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);

    }

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
