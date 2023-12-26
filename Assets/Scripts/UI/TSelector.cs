using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TSelector : MonoBehaviour
{
    [SerializeField] private GameObject tPoint;
    [SerializeField] private TextMeshProUGUI pointText;
    
    private Slider _slider;
    private BezierCurve _curve;
    private float _t;

    private void Awake()
    {
        _t = 0.5F;
        _slider = GetComponent<Slider>();
        _slider.value = _t;
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void Start()
    {
        DeCasteljauDrawer.OnDrawingChanged += OnDrawingChanged;
        PointManager.Instance.OnPointAdded += OnPointAdded;
        PointManager.Instance.OnLastPointRemoved += OnCurveChanged;
        
        foreach (var p in PointManager.Instance)
            p.OnPointChanged += OnCurveChanged;
        OnCurveChanged(null, null);
    }

    private void OnDrawingChanged(object sender, DeCasteljauDrawer.OnDrawingChangedArgs e)
    {
        _slider.interactable = !e.IsDrawing;
        tPoint.SetActive(!e.IsDrawing);
    }

    private void OnPointAdded(object sender, PointManager.OnPointArgs e)
    {
        e.Point.OnPointChanged += OnCurveChanged;
        OnCurveChanged(sender, e);
    }

    private void OnCurveChanged(object sender, EventArgs e)
    {
        _curve = PointManager.Instance.GetCurve();
        OnValueChanged(_t);
    }

    private void OnValueChanged(float value)
    {
        _t = value;
        var point = _curve.GetPoint(_t);
        tPoint.transform.position = point;
        pointText.text = $"t = {_t:F2} \u2192 ({point.z:F2}, {point.x:F2}, {point.y:F2})";
    }
}
