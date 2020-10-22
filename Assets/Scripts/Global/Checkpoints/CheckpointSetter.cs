// #define DIAGNOSTIC_MODE
using System;
using System.Collections;
using UnityEngine;

public class CheckpointSetter : MonoBehaviour
{
    #region Serialized Fields
#pragma warning disable CS0649
    // public GameObject trigger;
#pragma warning restore CS0649
    #endregion

    private GameObject _player;
#region Monobehaviour
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
       // trigger.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != _player)
            return;
        CheckpointManager.Instance.StartPosition = gameObject.transform.position;
        StartCoroutine(ActivateTrigger());
    }

    private IEnumerator ActivateTrigger()
    {
        yield return null;
    }
    #endregion

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
