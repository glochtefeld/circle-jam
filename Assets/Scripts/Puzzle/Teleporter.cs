// #define DIAGNOSTIC_MODE
using UnityEngine;
using WOB.Player;

public class Teleporter : MonoBehaviour
{
    public Transform destination;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BasePlayer>() == null)
            return;
        other.transform.position = destination.position;
    }

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
