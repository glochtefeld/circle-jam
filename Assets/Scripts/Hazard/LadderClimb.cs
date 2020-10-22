using UnityEngine;
using WOB.Player;

public class LadderClimb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BasePlayer>() != null)
            collision.GetComponent<BasePlayer>().StartClimbing();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<BasePlayer>() != null)
            collision.GetComponent<BasePlayer>().StartWalking();
    }
}
