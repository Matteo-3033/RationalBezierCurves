using UnityEngine;

public class CameraDistance : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera topCamera;
    
    [SerializeField] private float speed = 1.0F;
    [SerializeField] private Transform target;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float maxDistance = 10f;

    private void Update()
    {
        var toTarget = target.position - mainCamera.transform.position;
        var delta = InputManager.Instance.GetCameraDistance();
        
        switch (delta)
        {
            case > 0 when toTarget.magnitude <= minDistance:
            case < 0 when toTarget.magnitude >= maxDistance:
                return;
            default:
                mainCamera.transform.position += speed * delta * Time.deltaTime * toTarget.normalized;
                topCamera.orthographicSize -= speed * delta * Time.deltaTime;
                break;
        }
    }
}
