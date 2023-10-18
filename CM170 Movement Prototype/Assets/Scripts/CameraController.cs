using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera follow guide:
    // https://www.youtube.com/watch?v=MFQhpwc6cKE

    public Transform playerPositionRef;
    public Vector3 cameraOffset;

    // Camera boundaries and settings. Tweak as needed
    private float smoothingSpeed = 0.125f;
    private float minPositionX = -10.0f; // left border
    private float maxPositionX = 99.0f; // right border
    private float minPositionY = 0.0f;   // bottom border
    private float maxPositionY = 0.0f;   // top border

    void LateUpdate()
    {
        // Update camera to follow player
        Vector3 targetPosition = playerPositionRef.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothingSpeed);
        transform.position = smoothedPosition;

        // Help from: 
        // https://forum.unity.com/threads/best-way-to-stop-the-camera-from-at-edge-of-level.34421/
        // Clamp camera to desired boundaries
        Vector3 clampedTransform = transform.position;
        clampedTransform.x = Mathf.Clamp(clampedTransform.x, minPositionX, maxPositionX);
        clampedTransform.y = Mathf.Clamp(clampedTransform.y, minPositionY, maxPositionY);
        transform.position = clampedTransform;
    }

    void SetNewCameraBoundaries(float newMinPositionX, float newMaxPositionX, float newMinPositionY, float newMaxPositionY) 
    {
        minPositionX = newMinPositionX;
        maxPositionX = newMaxPositionX;
        minPositionY = newMinPositionY;
        maxPositionY = newMaxPositionY;
    }
}
