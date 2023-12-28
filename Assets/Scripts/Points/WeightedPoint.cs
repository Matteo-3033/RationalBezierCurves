using System;
using UnityEngine;

public class WeightedPoint: MonoBehaviour
{
    [SerializeField] private Transform pointObj;
    [SerializeField] private Transform weightedPointObj;
    
    public string Name { get; private set; } = "";
    
    
    private float _weight = 1F;
    public float Weight
    {
        get => _weight;
        set => SetWeight(value);
    }
    public Vector3 WeightedPosition => Weight * pointObj.position;
    
    
    public Vector2 UPosition
    {
        get => new(pointObj.position.z, pointObj.position.x);
        set => SetPosition(value);
    }
    public Vector3 Position => pointObj.position;
    
    
    public event EventHandler<OnPointChangedArgs> OnPointChanged;
    public class OnPointChangedArgs : EventArgs
    {
        public readonly WeightedPoint Point;
        public readonly bool PositionChanged;
        public readonly bool WeightChanged;

        public OnPointChangedArgs(WeightedPoint point, bool positionChanged, bool weightChanged)
        {
            Point = point;
            PositionChanged = positionChanged;
            WeightChanged = weightChanged;
        }
    }
    
    public void Init(string pointName, Vector2 point, float weight)
    {
        _weight = weight;
        Name = pointName;
        SetPosition(point);
    }
    
    private void SetPosition(Vector2 position)
    {
        if (position.x < -1) position.x = -1F;
        else if (position.x > 1) position.x = 1F;
        position.x = Mathf.Round(position.x * 1000000) / 1000000;
            
        if (position.y < -1) position.y = -1F;
        else if (position.y > 1) position.y = 1F;
        position.y = Mathf.Round(position.y * 1000000) / 1000000;
        
        pointObj.position = new Vector3(position.y, 1F, position.x);
        SetWeight(_weight, true);
    }
    
    private void SetWeight(float weight, bool positionChanged = false)
    {
        weight = Mathf.Round(weight * 1000000) / 1000000;
        var weightChanged = weight != _weight;
        _weight = weight;

        weightedPointObj.transform.position = WeightedPosition;
   
        OnPointChanged?.Invoke(this, new OnPointChangedArgs(this, positionChanged, weightChanged));
    }
}