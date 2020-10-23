// #define DIAGNOSTIC_MODE
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
#region Serialized Fields
#pragma warning disable CS0649

#pragma warning restore CS0649
#endregion

#region Monobehaviour
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    #endregion

    public void UnlockDoor() => gameObject.SetActive(false);

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
