using System.Collections;
using UnityEngine;
using WOB.UI;

public class TitleScreenEffects : MonoBehaviour
{
    #region Serialized Fields
    public Transform spaceShip;
    public Camera mainCam;
    public ParticleSystem planetCollision;
    [Header("Pulsing Text")]
    public CanvasGroup continueText;
    [Range(0f,0.01f)]
    public float rate;
    public float textHangTime;
    public float deadTime;
    [Header("Bobbing Spaceship")]
    public float height;
    public float shipLerpTime;
    public float shipHangTime;
    [Header("Camera Pan")]
    public float distance;
    public float cameraLerpTime;
    public float fadeInSpeed;
    [Header("Constant Canvases")]
    public Canvas mainCanvas;
    [Header("Audio")]
    public AudioSource sfx;
    public AudioClip confirm;
    #endregion

    private IEnumerator _pulse;
    private IEnumerator _bobbing;
    private CanvasSwitcher _canvasSwitcher;
    private bool _leftTitle = false;
    
    #region Monobehaviour
    private void Start()
    {
        _canvasSwitcher = gameObject.GetComponent<CanvasSwitcher>();
        _pulse = PulseCanvas(continueText);
        _bobbing = BobTransform(spaceShip);
        StartCoroutine(_pulse);
        StartCoroutine(_bobbing);
    }

    private void Update()
    {
        if (!_leftTitle && Input.anyKeyDown)
        {
            // Debug.Log($"Any key pressed");
            SwitchToMainMenu();
        }
    }
    #endregion

    private void SwitchToMainMenu()
    {
        sfx.PlayOneShot(confirm);
        _leftTitle = true;
        StopCoroutine(_pulse);
        StopCoroutine(_bobbing);
        _canvasSwitcher.DeactivateCurrentCanvas();
        // Cannot execute code after this
        StartCoroutine(PanCamera());
    }

    private IEnumerator PulseCanvas(CanvasGroup cg)
    {
        while (true)
        {
            while (cg.alpha > 0)
            {
                cg.alpha -= rate;
                yield return null;
            }
            cg.alpha = 0;
            yield return new WaitForSeconds(deadTime);
            while (cg.alpha < 1)
            {
                cg.alpha += rate;
                yield return null;
            }
            cg.alpha = 1;
            yield return new WaitForSeconds(textHangTime);
        }
    }

    private IEnumerator BobTransform(Transform t)
    {
        bool atBottomOfBob = true;
        while (true)
        {
            float currentTime = 0f;
            var startPos = t.position;
            if (atBottomOfBob)
            {
                var targetPosition = t.position + new Vector3(0,height,0);
                while (t.position.y < targetPosition.y - 0.1f)
                {
                    currentTime += Time.deltaTime;
                    var ratio = currentTime / shipLerpTime;
                    t.position = Vector3.Lerp(startPos, targetPosition, ratio);
                    yield return null;
                }
            }
            else
            {
                var targetPosition = t.position - new Vector3(0, height, 0);
                while (t.position.y > targetPosition.y + 0.1f)
                {
                    currentTime += Time.deltaTime;
                    var ratio = currentTime / shipLerpTime;
                    t.position = Vector3.Lerp(startPos, targetPosition, ratio);
                    yield return null;
                }
            }
            yield return new WaitForSeconds(shipHangTime);
            atBottomOfBob = !atBottomOfBob;
        }
    }

    private IEnumerator PanCamera()
    {
        float currentTime = 0f;
        var currentPos = mainCam.transform.position;
        var targetPos = new Vector3(distance, 0, mainCam.transform.position.z);
        while (currentTime < cameraLerpTime - 0.1f)
        {
            currentTime += Time.deltaTime;
            var ratio = currentTime / cameraLerpTime;
            mainCam.transform.position = 
                Vector3.Lerp(currentPos, targetPos, ratio);
            yield return null;
        }
        _canvasSwitcher.SwitchCanvasFade(
            mainCanvas,
            fadeInSpeed);
    }
}
/* "Animates" the title screen effects. The performance hit of 
 * checking for input on update is fairly minimal, and will be 
 * short circuited quickly. */