//#define DIAGNOSTIC_MODE
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { private set; get; }
    public Vector3 StartPosition { set; get; }

    #region Monobehaviour
    private void Awake()
    {
        if (Instance == null)
        {
            StartPosition = gameObject.transform.position;
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    #endregion

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"Start: {StartPosition}");
    }
#endif
}
