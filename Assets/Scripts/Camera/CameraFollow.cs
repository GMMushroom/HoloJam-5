using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private float _fixedCameraY;
    private float _fixedCameraZ;

    void Start()
    {
        _fixedCameraY = transform.position.y;
        _fixedCameraZ = transform.position.z;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, _fixedCameraY + offset.y, _fixedCameraZ + offset.z);
        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        transform.position = smoothedPosition;
    }
}
//TODO: Remove script with Cinemachine.
