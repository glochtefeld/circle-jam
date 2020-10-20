using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraMovement : MonoBehaviour
{
    #region Serialized Fields
    [Header("Follow")]
    [Tooltip("Insert The GameObject You Want To Follow")]
    public GameObject Player;
    [Range(0f,1f)]
    public float Smoothness;
    #endregion


    private Vector3 _velocity;

    void Start()
    {
        _velocity = Vector3.zero;
    }


    void FixedUpdate()
    {
        var targetPosition = new Vector3(
            Player.transform.position.x, 
            Player.transform.position.y, 
            transform.position.z);

        transform.position = Vector3.SmoothDamp(
            transform.position, 
            targetPosition,
            ref _velocity, 
            Smoothness);
    }
}