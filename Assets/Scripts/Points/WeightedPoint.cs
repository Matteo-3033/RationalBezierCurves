using System;
using UnityEngine;

public class WeightedPoint: MonoBehaviour
{
    [SerializeField] private Transform pointObj;
    [SerializeField] private Transform weightObj;
    
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
    
    
    public event EventHandler<PointManager.OnPointArgs> OnPointChanged;
    
    
    public void Init(string pointName, Vector2 point, float weight)
    {
        _weight = weight;
        Name = pointName;
        SetPosition(point);
    }
    
    private void SetPosition(Vector2 position)
    {
        if (position.x < 0) position.x = 0F;
        else if (position.x >= 1) position.x = 0.999F;
        position.x = Mathf.Round(position.x * 1000) / 1000;
            
        if (position.y < 0) position.y = 0F;
        else if (position.y >= 1) position.y = 0.999F;
        position.y = Mathf.Round(position.y * 1000) / 1000;
        
        pointObj.position = new Vector3(position.y, 1F, position.x);
        UpdateWeight();
    }
    
    private void SetWeight(float weight)
    {
        weight = Mathf.Round(weight * 1000) / 1000;
        _weight = weight;

        weightObj.transform.position = WeightedPosition;
   
        OnPointChanged?.Invoke(this, new PointManager.OnPointArgs(this));
    }

    private void UpdateWeight()
    {
        SetWeight(_weight);
    }
}