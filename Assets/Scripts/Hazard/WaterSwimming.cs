using UnityEngine;
using WOB.Player;

namespace WOB.Hazard
{
    public class WaterSwimming : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<BasePlayer>() != null)
                collision.GetComponent<BasePlayer>().StartSwimming();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<BasePlayer>() != null)
                collision.GetComponent<BasePlayer>().StartWalking();
        }
    }
}