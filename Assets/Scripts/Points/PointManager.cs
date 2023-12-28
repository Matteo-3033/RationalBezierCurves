using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointManager : MonoBehaviour, IEnumerable<WeightedPoint>
{
    public enum Preset
    {
        Iperbole,
        ArcoEllisse,
        ArcoEllisseInverso,
        ArcoCirconferenza,
        Circonferenza,
    }
    
    private const int MAX_POINTS = 10;
    private const int MIN_POINTS = 2;
    
    [SerializeField] private GameObject weightedPointPrefab;

    private readonly List<WeightedPoint> _points = new();
    
    public event EventHandler<OnPointArgs> OnPointAdded;
    public event EventHandler<EventArgs> OnLastPointRemoved;
    public class OnPointArgs : EventArgs
    {
        public readonly WeightedPoint Point;
        public OnPointArgs(WeightedPoint point) => Point = point;
    }
    
    public event EventHandler<EventArgs> OnWeightsNormalized;

    public int Count => _points.Count;
    public bool IsFull() => _points.Count >= MAX_POINTS;
    public bool IsMinimum() => _points.Count <= MIN_POINTS;
    
    public static PointManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (Settings.Preset == null)
        {
            AddPoint(new Vector2(-0.6F, -0.6F), 2F);
            AddPoint(new Vector2(0F, 0.6F), 1.5F);
            AddPoint(new Vector2(0.6F, -0.6F), 2F);
        }
        else LoadPreset();
    }

    private void LoadPreset()
    {
        switch (Settings.Preset)
        {
            case Preset.Iperbole:
                Iperbole();
                break;
            case Preset.ArcoEllisse:
                ArcoEllisse();
                break;
            case Preset.ArcoEllisseInverso:
                ArcoEllisseInverso();
                break;
            case Preset.ArcoCirconferenza:
                ArcoCirconferenza();
                break;
            case Preset.Circonferenza:
                Circonferenza();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Iperbole()
    {
        AddPoint(new Vector2(0.2F, 0.8F), 1F);
        AddPoint(new Vector2(0.8F, 0.5F), 2F);
        AddPoint(new Vector2(0.2F, 0.2F), 1F);
    }

    private void ArcoEllisse()
    {
        AddPoint(new Vector2(0.2F, 0.8F), 1F);
        AddPoint(new Vector2(0.8F, 0.5F), 0.5F);
        AddPoint(new Vector2(0.2F, 0.2F), 1F);
    }

    private void ArcoEllisseInverso()
    {
        AddPoint(new Vector2(0.2F, 0.8F), 1F);
        AddPoint(new Vector2(0.8F, 0.5F), -0.5F);
        AddPoint(new Vector2(0.2F, 0.2F), 1F);
    }

    private void ArcoCirconferenza()
    {
        AddPoint(new Vector2(0.354F, 0.146F), 1F);
        AddPoint(new Vector2(0.707F, 0.5F), 0.707F);
        AddPoint(new Vector2(0.354F, 0.854F), 1F);
    }

    private void Circonferenza()
    {
        AddPoint(new Vector2(0.354F, 0.146F), 1F);
        AddPoint(new Vector2(0.707F, 0.5F), 0.707F);
        AddPoint(new Vector2(0.354F, 0.854F), 1F);
    }

    private void AddPoint(Vector2 position, float weight)
    {
        var obj = Instantiate(weightedPointPrefab, transform);
        var point = obj.GetComponent<WeightedPoint>();
        point.Init(((char)('A' + _points.Count)).ToString(), position, weight);

        _points.Add(point);
        
        OnPointAdded?.Invoke(this, new OnPointArgs(point));
    }

    public void NewPoint()
    {
        if (IsFull()) return;
        
        var n = Count - 1;
        AddPoint(_points.Last().UPosition, _points.Last().Weight * (n + 1));
        
        var newWeights = new float[n + 2];
        var newPoints = new Vector2[n + 2];
        
        for (var i = 1; i <= n; i++)
        {
            var p = _points[i];
            var prevP = _points[i - 1];
            
            newWeights[i] = i * prevP.Weight + (n + 1 - i) * p.Weight;
            newPoints[i] = i * prevP.Weight * prevP.UPosition + (n + 1 - i) * p.Weight * p.UPosition;
            newPoints[i] /= newWeights[i];
        }

        for (var i = 1; i <= n; i++)
        {
            _points[i].Weight = newWeights[i];
            _points[i].UPosition = newPoints[i];
        }

        _points[0].Weight *= n + 1;
        
        NormalizeWeights();
    }
    
    public void RemovePoint()
    { 
        if (IsMinimum()) return;

        var n = Count - 1;
        var bLefts = BLefts(n);
        var bRights = BRights(n);

        var pow = (float) Math.Pow(2, 1 - 2 * n);
        var alpha = pow; 
        for (var i = 0; i <= n - 1; i++)
        {
            alpha += pow * Factorial.Binomial(2 * n, 2 * i);
            _points[i].UPosition = (1 - alpha) * bLefts[i] + alpha * bRights[i];
        }
        
        var point = _points.Last();
        _points.RemoveAt(Count - 1);
        OnLastPointRemoved?.Invoke(this, EventArgs.Empty);
        Destroy(point.gameObject);
    }

    private Vector2[] BLefts(int n)
    {
        var res = new Vector2[n];
        res[0] = n * _points[0].UPosition / n;
        for (var i = 1; i <= n - 1; i++)
            res[i] = (n * _points[i].UPosition - i * res[i - 1]) / (n - i);
        return res;
    }
    
    private Vector2[] BRights(int n)
    {
        var res = new Vector2[n];
        res[n - 1] = n * _points[n].UPosition / n;
        for (var i = n - 1; i >= 1; i--)
            res[i - 1] = (n * _points[i].UPosition - (n - i) * res[i]) / i;
        return res;
    }

    public void NormalizeWeights()
    {
        var c = (float) Math.Pow(_points.First().Weight / _points.Last().Weight, 1F / Count);
        var ci = 1F;
        var firstW = _points.First().Weight;
        foreach (var p in _points)
        {
            p.Weight *= ci / firstW;
            ci *= c;
        }
        
        OnWeightsNormalized?.Invoke(this, EventArgs.Empty);
    }
    
    public BezierCurve GetCurve()
    {
        return new BezierCurve(_points);
    }

    public WeightedPoint this[int index] => _points[index];

    public IEnumerator<WeightedPoint> GetEnumerator()
    {
        return _points.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}