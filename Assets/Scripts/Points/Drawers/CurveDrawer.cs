using System;
using UnityEngine;

public class CurveDrawer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    
    private void UpdateCurve()
    {
        var curve = PointManager.Instance.GetCurve();
        for (var i = 0; i < _lineRenderer.positionCount; i++)
        {
            var t = (float) i / (_lineRenderer.positionCount - 1);
            _lineRenderer.SetPosition(i, curve.GetPoint(t));
        }
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 1000;
    }

    private void Start()
    {
        PointManager.Instance.OnPointAdded += OnPointAdded;
        PointManager.Instance.OnLastPointRemoved += OnPointRemoved;
        foreach (var p in PointManager.Instance)
            p.OnPointChanged += OnPointChanged;
        UpdateCurve();
    }

    private void OnPointAdded(object sender, PointManager.OnPointArgs e)
    {
        e.Point.OnPointChanged += OnPointChanged;
        UpdateCurve();
    }

    private void OnPointRemoved(object sender, EventArgs e)
    {
        UpdateCurve();
    }

    private void OnPointChanged(object sender, EventArgs e)
    {
        UpdateCurve();
    }
}