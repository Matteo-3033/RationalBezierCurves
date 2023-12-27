using System;
using System.Collections.Generic;
using UnityEngine;

public class DeCasteljauUDrawer : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;

    private readonly List<GameObject> _objs = new();
    private float _t = 0.5F;
        
    private void UpdateDrawing()
    {
        if (!Settings.ShowDeCasteljau)
            return;
        
        InitPoints(out var points, out var weights);

        while (points.Count > 1)
        {
            DrawLines(points);
            UpdatePoints(points, weights, out points, out weights);
        }
    }
    
    private void InitPoints(out List<GameObject> points, out List<float> weights)
    {
        _objs.ForEach(Destroy);
        _objs.Clear();
        
        points = new List<GameObject>();
        weights = new List<float>();
        
        for (var i = 0; i < PointManager.Instance.Count - 1; i++)
        {
            var p1 = PointManager.Instance[i];
            var p2 = PointManager.Instance[i + 1];

            var weight = Mathf.Lerp(p1.Weight, p2.Weight, _t);
            var pos = Vector3.Lerp(p1.WeightedPosition, p2.WeightedPosition, _t) / weight;
            
            var point = Instantiate(pointPrefab, pos, Quaternion.identity, transform);
            point.layer = gameObject.layer;
            
            _objs.Add(point);
            points.Add(point);
            weights.Add(weight);
        }
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
    
    private void UpdatePoints(IReadOnlyList<GameObject> points, IReadOnlyList<float> weights, out List<GameObject> newPoints, out List<float> newWeights)
    {
        newPoints = new List<GameObject>();
        newWeights = new List<float>();
        
        for (var i = 0; i < points.Count - 1; i++)
        {
            var p1 = points[i];
            var w1 = weights[i];
            var p2 = points[i + 1];
            var w2 = weights[i + 1];
            
            var newWeight = Mathf.Lerp(w1, w2, _t);
            var newPos = Vector3.Lerp(w1 * p1.transform.position, w2 * p2.transform.position, _t) / newWeight;
            
            var newPoint = Instantiate(pointPrefab, newPos, Quaternion.identity, transform);
            newPoint.layer = gameObject.layer;

            _objs.Add(newPoint);
            newPoints.Add(newPoint);
            newWeights.Add(newWeight);
        }
    }
    
    private void Start()
    {
        PointManager.Instance.OnPointAdded += OnPointAdded;
        PointManager.Instance.OnLastPointRemoved += OnPointRemoved;
        TSelector.OnSelectedTChanged += OnTChanged;
        Settings.OnShowDeCasteljauChanged += OnShowDeCasteljauChanged;
        
        foreach (var p in PointManager.Instance)
            p.OnPointChanged += OnPointChanged;
        UpdateDrawing();
    }

    private void OnShowDeCasteljauChanged(object sender, Settings.OnShowDeCasteljauArgs e)
    {
        if (e.Show)
            UpdateDrawing();
        else
        {
            _objs.ForEach(Destroy);
            _objs.Clear();
        }
    }

    private void OnTChanged(object sender, TSelector.OnSelectedTChangedArgs e)
    {
        _t = e.T;
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

    private void OnPointChanged(object sender, EventArgs e)
    {
        UpdateDrawing();
    }
}
