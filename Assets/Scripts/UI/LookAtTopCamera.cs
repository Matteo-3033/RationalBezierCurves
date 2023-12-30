using System;
using UnityEngine;

public class LookAtTopCamera : MonoBehaviour
{
    [SerializeField] private Mode mode;
    private Camera _camera;

    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    private void Awake()
    {
        _camera = Camera.allCameras[1];
    }

    private void Start()
    {
        transform.forward = -_camera.transform.forward;
    }
}
