using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraMovement : MonoBehaviour
{
    #region Serialized Fields
    [Header("Follow")]
    [Tooltip("Insert The GameObject You Want To Follow")]
    public GameObject Player;
    [Space]
    public Vector3 PlayerPosition;
    [Space]
    public Vector3 Velocity;
    [Space]
    public float Smoothness;
    #endregion




    void Start()
    {
        Velocity = Vector3.zero;
    }


    void Update()
    {
        PlayerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, PlayerPosition, ref Velocity, Smoothness);
    }
}