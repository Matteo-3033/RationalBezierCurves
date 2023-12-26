using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeightDragger : MonoBehaviour
{
    [SerializeField] private WeightedPoint point;

    private bool _moving;
    private Vector3? _mousePos;

    private void Update()
    {
        if (!_moving) return;
        var newMousePos = Input.mousePosition;
        var delta = (newMousePos - _mousePos!.Value).y;
        point.Weight += delta / 500;
        _mousePos = newMousePos;
    }

    private void OnMouseDown()
    {
        if (DeCasteljauDrawer.IsDrawing) return;
        _moving = true;
        _mousePos = Input.mousePosition;
    }
    
    private void OnMouseUp()
    {
        _moving = false;
        _mousePos = null;
    }
}