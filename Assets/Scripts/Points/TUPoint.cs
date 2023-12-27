using UnityEngine;

public class TUPoint : MonoBehaviour
{
    [SerializeField] private Transform derivative;
    
    private Vector2 _derivative;

    public Vector2 Derivative
    {
        get => _derivative;
        set => SetDerivative(value);
    }

    private void SetDerivative(Vector2 value)
    {
        Debug.Log(value);
        _derivative = value;
        derivative.forward = new Vector3(value.y, derivative.forward.y, value.x);
    }
}