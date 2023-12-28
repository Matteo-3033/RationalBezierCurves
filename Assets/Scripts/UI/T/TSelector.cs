using System;
using UnityEngine;
using UnityEngine.UI;

public class TSelector : MonoBehaviour
{
    private BezierCurve _curve;
    private float _t;

    public static Slider Slider { get; private set; }

    public static event EventHandler<OnSelectedTChangedArgs> OnSelectedTChanged;
    
    public class OnSelectedTChangedArgs : EventArgs
    {
        public readonly float T;
        public readonly Vector3 P;
        
        public OnSelectedTChangedArgs(float t, Vector3 point)
        {
            T = t;
            P = point;
        }
    }

    private void Awake()
    {
        _t = 0.5F;
        Slider = GetComponent<Slider>();
        Slider.value = _t;
        Slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void Start()
    {
        PointManager.Instance.OnPointAdded += OnPointAdded;
        PointManager.Instance.OnLastPointRemoved += OnCurveChanged;
        
        foreach (var p in PointManager.Instance)
            p.OnPointChanged += OnCurveChanged;
        OnCurveChanged(null, null);
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
        var p = _curve.GetPoint(_t);
        OnSelectedTChanged?.Invoke(this, new OnSelectedTChangedArgs(_t, p));
    }

    private void OnDestroy()
    {
        if (PointManager.Instance == null) return;
        
        PointManager.Instance.OnPointAdded -= OnPointAdded;
        PointManager.Instance.OnLastPointRemoved -= OnCurveChanged;
        
        foreach (var p in PointManager.Instance)
            p.OnPointChanged -= OnCurveChanged;
    }
}
