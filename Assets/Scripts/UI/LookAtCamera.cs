using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
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
        _camera = Camera.allCameras[0];
    }

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(_camera.transform);
                break;
            case Mode.LookAtInverted:
                var dir = transform.position - _camera.transform.position;
                transform.LookAt(transform.position + dir);
                break;
            case Mode.CameraForward:
                transform.forward = _camera.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -_camera.transform.forward;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
