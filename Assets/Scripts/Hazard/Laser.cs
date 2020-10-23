// #define DIAGNOSTIC_MODE
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using WOB.Player;

public class Laser : MonoBehaviour
{
    #region Serialized Fields
#pragma warning disable CS0649
    public ParticleSystem ps;
#pragma warning restore CS0649
    #endregion
    // private ParticleCollisionEvent[] events;
    private bool _stopped = false;
    #region Monobehaviour
    private void Start()
    {
       // events = new ParticleCollisionEvent[8];
    }
    private void OnParticleCollision(GameObject other)
    {
        //int collCount = ps.GetSafeCollisionEventSize();
        //if (collCount > events.Length)
        //    events = new ParticleCollisionEvent[collCount];
        //int eventCount = ps.GetCollisionEvents(other, events);
        var player = other.GetComponent<BasePlayer>();
        //for (int i = 0; i < eventCount; i++)
        //{
        //    if (player != null)
        //        player.Kill();
        //}
        if (player != null && !_stopped)
        {
            try
            {
                _stopped = true; 
                Debug.Log($"No Error 1");
                ps.Stop();
                Debug.Log($"No Error 2");
                StartCoroutine(KillPlayer(player));
                
            } catch { }
            
        }
    }
    #endregion
    private IEnumerator KillPlayer(BasePlayer player)
    {
        Debug.Log($"No Error 3");
        yield return new WaitForSeconds(0.05f);
        Debug.Log($"No Error 4");
        player.Kill();
        Debug.Log($"No Error 5");
    }
#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
