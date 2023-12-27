using System;
using UnityEngine;

public class ControlPolygonUDrawer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    
    private void UpdatePolygon()
    {
        for (var i = 0; i < PointManager.Instance.Count; i++)
            _lineRenderer.SetPosition(i, PointManager.Instance[i].Position);
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        PointManager.Instance.OnPointAdded += OnPointAdded;
        PointManager.Instance.OnLastPointRemoved += OnPointRemoved;
        Settings.OnShowControlPolygonChanged += OnShowControlPolygonChanged;
        
        foreach (var p in PointManager.Instance)
            p.OnPointChanged += OnPointChanged;
        _lineRenderer.positionCount = PointManager.Instance.Count;
        UpdatePolygon();
    }

    private void OnShowControlPolygonChanged(object sender, Settings.OnShowControlPolygonArgs e)
    {
        _lineRenderer.enabled = e.Show;
    }

    private void OnPointAdded(object sender, PointManager.OnPointArgs e)
    {
        e.Point.OnPointChanged += OnPointChanged;
        _lineRenderer.positionCount = PointManager.Instance.Count;
        UpdatePolygon();
    }

    private void OnPointRemoved(object sender, EventArgs e)
    {
        _lineRenderer.positionCount = PointManager.Instance.Count;
        UpdatePolygon();
    }

    private void OnPointChanged(object sender, EventArgs e)
    {
        UpdatePolygon();
    }
}