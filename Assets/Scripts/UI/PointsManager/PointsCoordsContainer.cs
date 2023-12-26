using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointsCoordsContainer : MonoBehaviour
{
    [SerializeField] private GameObject pointCoordsTemplate;
    
    private readonly List<GameObject> _rows = new();

    private void Awake()
    {
        pointCoordsTemplate.SetActive(false);
    }

    private void Start()
    {
        PointManager.Instance.OnPointAdded += OnPointAdded;
        PointManager.Instance.OnLastPointRemoved += OnLastPointRemoved;

        foreach (var p in PointManager.Instance)
            OnPointAdded(null, new PointManager.OnPointArgs(p));
    }
    
    private void OnPointAdded(object sender, PointManager.OnPointArgs args)
    {
        var obj = Instantiate(pointCoordsTemplate, transform);
        obj.SetActive(true);
        _rows.Add(obj);
        var pointCoords = obj.GetComponent<PointCoords>();
        pointCoords.Point = args.Point;
    }
    
    private void OnLastPointRemoved(object sender, EventArgs args)
    {
        var row = _rows.Last();
        Destroy(row);
        _rows.RemoveAt(_rows.Count - 1);
    }
}
