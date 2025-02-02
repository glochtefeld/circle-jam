﻿// #define DIAGNOSTIC_MODE
using UnityEngine;
using WOB.Player;

public class Pickup : MonoBehaviour
{
    #region Serialized Fields
#pragma warning disable CS0649

#pragma warning restore CS0649
    #endregion

    private bool _addedPoint;
    #region Monobehaviour
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<BasePlayer>();
        if (player == null || _addedPoint)
            return;
        player.AddPoints();
        player.audioControl.PlaySFX(SFX.GetTreasure);
        _addedPoint = true;
        Destroy(gameObject);
    }
    #endregion

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}