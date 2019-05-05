using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] GameObject startArea;
    Camera camera;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private float startFOV;

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        
        camera = GetComponent<Camera>();
        startFOV = camera.fieldOfView;
    }

    private void Update()
    {
        if (startArea.activeSelf)
            ResetLocation();
    }

    private void ResetLocation()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        camera.fieldOfView = startFOV;
    }
}
