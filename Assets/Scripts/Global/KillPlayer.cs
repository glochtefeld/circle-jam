// #define DIAGNOSTIC_MODE
using UnityEngine;
using WOB.Player;

public class KillPlayer : MonoBehaviour
{
    #region Serialized Fields
#pragma warning disable CS0649

#pragma warning restore CS0649
    #endregion

    #region Monobehaviour
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<BasePlayer>();
        if (player != null)
            player.Kill();
    }
    #endregion

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
