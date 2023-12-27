using System;
using UnityEngine;

public class WeightedPointLine: MonoBehaviour
{
    [SerializeField] private WeightedPoint point;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        point.OnPointChanged += OnPointChanged;
        OnPointChanged(this, new PointManager.OnPointArgs(point));
    }

    private void OnPointChanged(object sender, EventArgs e)
    {
        _lineRenderer.SetPosition(0, point.Position);
        _lineRenderer.SetPosition(1, point.WeightedPosition);
    }
}