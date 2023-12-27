using System;
using System.Collections.Generic;
using UnityEngine;

public class DeCasteljauDrawer : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;

    private List<GameObject> _objs = new();
    private float _t;
        
    private void UpdateDrawing()
    {
        var points = InitPoints();

        while (points.Count > 1)
        {
            DrawLines(points);
            points = UpdatePoints(points);
        }
    }

    public void SetT(float t)
    {
        if (t is < 0 or > 1) return;
        _t = t;
        UpdateDrawing();
    }
    
    private List<GameObject> InitPoints()
    {
        _objs.ForEach(Destroy);
        _objs = new List<GameObject>();
        
        var points = new List<GameObject>();
        
        for (var i = 0; i < PointManager.Instance.Count - 1; i++)
        {
            var p1 = PointManager.Instance[i];
            var p2 = PointManager.Instance[i + 1];
            var pos = Vector3.Lerp(p1.WeightedPosition, p2.WeightedPosition, _t);
            var point = Instantiate(pointPrefab, pos, Quaternion.identity, transform);
            
            _objs.Add(point);
            points.Add(point);
        }

        return points;
    }

    private void DrawLines(IReadOnlyList<GameObject> points)
    {
        for (var i = 0; i < points.Count - 1; i++)
        {
            var line = points[i].GetComponent<LineRenderer>();
            line.positionCount = 2;
            line.SetPosition(0, points[i].transform.position);
            line.SetPosition(1, points[i + 1].transform.position);
        }
    }
    
    private List<GameObject> UpdatePoints(IReadOnlyList<GameObject> points)
    {
        var newPoints = new List<GameObject>();
        for (var i = 0; i < points.Count - 1; i++)
        {
            var p1 = points[i];
            var p2 = points[i + 1];
            var newPos = Vector3.Lerp(p1.transform.position, p2.transform.position, _t);
            var newPoint = Instantiate(pointPrefab, newPos, Quaternion.identity, transform);
            newPoints.Add(newPoint);
            _objs.Add(newPoint);
        }

        return newPoints;
    }
    
    private void Start()
    {
        PointManager.Instance.OnPointAdded += OnPointAdded;
        PointManager.Instance.OnLastPointRemoved += OnPointRemoved;
        foreach (var p in PointManager.Instance)
            p.OnPointChanged += OnPointChanged;
        UpdateDrawing();
    }

    private void OnPointAdded(object sender, PointManager.OnPointArgs e)
    {
        e.Point.OnPointChanged += OnPointChanged;
        UpdateDrawing();
    }

    private void OnPointRemoved(object sender, EventArgs e)
    {
        UpdateDrawing();
    }

    private void OnPointChanged(object sender, PointManager.OnPointArgs e)
    {
        UpdateDrawing();
    }
}
