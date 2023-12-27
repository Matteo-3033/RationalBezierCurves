using UnityEngine;

public class TUPoint : MonoBehaviour
{
    [SerializeField] private Transform derivative;

    private void Start()
    {
        TSelector.OnSelectedTChanged += OnSelectedTChanged;
    }

    private void OnSelectedTChanged(object sender, TSelector.OnSelectedTChangedArgs e)
    {
        transform.position = e.P / e.P.y;
        //SetDerivative(PointManager.Instance.GetCurve().GetUDerivative(e.T));
    }

    private void SetDerivative(Vector2 value)
    {
        derivative.forward = new Vector3(value.y, derivative.forward.y, value.x);
    }
}