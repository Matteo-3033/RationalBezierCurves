using System;
using System.Collections.Generic;
using UnityEngine;

public class DeCasteljauDrawer : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;

    private List<GameObject> _points;
    private float _t;
    private bool _linesIsNext = false;

    private static bool _isDrawing;
    public static bool IsDrawing
    {
        get => _isDrawing;
        private set
        {
            _isDrawing = value;
            OnDrawingChanged?.Invoke(null, new OnDrawingChangedArgs());
        }
    }

    public static event EventHandler<OnDrawingChangedArgs> OnDrawingChanged;

    public class OnDrawingChangedArgs : EventArgs
    {
        public bool IsDrawing = DeCasteljauDrawer.IsDrawing;
    }

    public void Init(float t)
    {
        if (IsDrawing) return;
        
        _t = t;
        var curve = PointManager.Instance.GetCurve();
        _points = new List<GameObject>();
        
        for (var i = 0; i < PointManager.Instance.Count - 1; i++)
        {
            var p1 = PointManager.Instance[i];
            var p2 = PointManager.Instance[i + 1];
            var pos = Vector3.Lerp(p1.WeightedPosition, p2.WeightedPosition, _t);
            var point = Instantiate(pointPrefab, pos, Quaternion.identity, transform);
            _points.Add(point);
        }
        
        _linesIsNext = true;
        IsDrawing = true;
    }
    
    public void Next()
    {
        if (!IsDrawing || _points.Count <= 1) return;
        
        if (_linesIsNext) DrawLines();
        else NewPoints();
        
        _linesIsNext = !_linesIsNext;
        
        if (_points.Count <= 1)
        {
            IsDrawing = false;
            _points.ForEach(Destroy);
        }
    }

    private void DrawLines()
    {
        for (var i = 0; i < _points.Count - 1; i++)
        {
            var line = _points[i].GetComponent<LineRenderer>();
            line.positionCount = 2;
            line.SetPosition(0, _points[i].transform.position);
            line.SetPosition(1, _points[i + 1].transform.position);
        }
    }
    
    private void NewPoints()
    {
        var newPoints = new List<GameObject>();
        for (var i = 0; i < _points.Count - 1; i++)
        {
            var p1 = _points[i];
            var p2 = _points[i + 1];
            var newPos = Vector3.Lerp(p1.transform.position, p2.transform.position, _t);
            var newPoint = Instantiate(pointPrefab, newPos, Quaternion.identity, transform);
            newPoints.Add(newPoint);
        }

        _points.ForEach(Destroy);
        _points = newPoints;
    }
}
