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

    private Vector3 _velocity;
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
        var target = new Vector3(0, 1f, 0);
        yield return new WaitForSeconds(3f);
        var time = 0f;
        while (Vector3.Distance(creditsPanel.transform.position, target) > 0.01f)
        {
            time += Time.deltaTime;
            var ratio = time / 300f;
            creditsPanel.transform.position =
                Vector3.Lerp(creditsPanel.transform.position,
                target,
                ratio);
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
