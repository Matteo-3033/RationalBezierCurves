using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private float speed = 1.0F;
    [SerializeField] private Vector3 axis = Vector3.up;
    [SerializeField] private Transform target;
    
    public float TotalAngle { get; private set; }

    private void Update()
    {
        var delta = InputManager.Instance.GetCameraRotation();
        var angle = speed * delta * Time.deltaTime;
        transform.RotateAround(target.position, axis, angle);
        TotalAngle = (TotalAngle + angle) % 360;
    }
}
