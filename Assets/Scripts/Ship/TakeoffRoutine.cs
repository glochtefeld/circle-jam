// #define DIAGNOSTIC_MODE
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using WOB.Player;

public class TakeoffRoutine : MonoBehaviour
{
    #region Serialized Fields
#pragma warning disable CS0649
    public ParticleSystem idle;
    public ParticleSystem takeOff;
    public Animator animator;
    [Header("FadeToBlack")]
    public string nextScene;
    public string nextSceneTransition;
    public bool loadPlayer;
    
#pragma warning restore CS0649
    #endregion

    private CanvasGroup cg;
    private bool _started;
#region Monobehaviour
    void Start()
    {
        cg = GameObject.Find("/Canvas").transform.GetChild(0)
            .GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(FlyAwayAndEndLevel());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BasePlayer>()
            == null)
            return;
        StartCoroutine(FlyAwayAndEndLevel());
    }
    #endregion

    private IEnumerator FlyAwayAndEndLevel()
    {
        Debug.Log($"Starting ");
        animator.Play("Takeoff");
        idle.Pause();
        idle.gameObject.SetActive(false);
        var player = GameObject.Find("Player");
        player.SetActive(false);
        var time = 0f;
        while (time < 0.417f)
        {
            time += Time.deltaTime;
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + 0.001f,
                transform.position.z);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        animator.enabled = false;
        takeOff.Play();

        Camera.main.GetComponent<SmoothCameraMovement>()
            .Player = gameObject;
        time = 0;
        var rate = 0.001f;
        cg.gameObject.SetActive(true);
        while (time < 3f)
        {
            time += Time.deltaTime;
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + 0.02f,
                transform.position.z);

            cg.alpha += rate;
            yield return null;
        }
        cg.alpha = 1f;

        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;
        if (loadPlayer)
        {
            SceneManager.LoadScene("Player");
            SceneManager.LoadScene(nextScene, LoadSceneMode.Additive);
            if (nextSceneTransition != "")
                SceneManager.LoadScene(nextSceneTransition, LoadSceneMode.Additive);
        }
        else
            SceneManager.LoadScene(nextScene);
    }
#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
