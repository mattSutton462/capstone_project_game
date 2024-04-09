using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player's Transform
    public float smoothSpeed = 0.125f; // Speed of the camera following
    public Vector3 offset; // Offset of the camera relative to the player

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10); // -10 for the camera's z-position in 2D
    }
}

