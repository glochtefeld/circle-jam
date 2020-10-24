// #define DIAGNOSTIC_MODE
using UnityEngine;

public class MusicAssigner : MonoBehaviour
{
    #region Serialized Fields
#pragma warning disable CS0649
    public BGM music;
#pragma warning restore CS0649
#endregion

    public static MusicAssigner Instance { set; get; }
    #region Monobehaviour
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            GameObject.Find("/Player").GetComponentInChildren<AudioControl>().PlayBGM(music);
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        
    }
#endregion

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
