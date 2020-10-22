// #define DIAGNOSTIC_MODE
using UnityEngine;
using WOB.Player;

public class EndOfLevel : MonoBehaviour
{
    #region Serialized Fields
#pragma warning disable CS0649

#pragma warning restore CS0649
    #endregion

    #region Monobehaviour
    
    #endregion

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
