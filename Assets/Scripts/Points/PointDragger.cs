﻿using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PointDragger : MonoBehaviour
{
    [SerializeField] private WeightedPoint point;

    private bool _moving;
    private Vector3? _mousePos;
    private RotateAround _camera;

    private void Start()
    {
        _camera = Camera.allCameras[0].gameObject.GetComponent<RotateAround>();
    }

    private void Update()
    {
        if (!_moving) return;
        var newMousePos = Input.mousePosition;
        var delta = (newMousePos - _mousePos!.Value) / 500;
        //Debug.Log(_camera.TotalAngle);
        //Debug.Log(delta);
        
        delta = Quaternion.Euler(0, -_camera.TotalAngle, 0) * delta;
        //Debug.Log(delta);
        point.UPosition += new Vector2(delta.x, -delta.y);
        _mousePos = newMousePos;
    }

    private void OnMouseDown()
    {
        if (DeCasteljauDrawer.IsDrawing) return;
        _moving = true;
        _mousePos = Input.mousePosition;
    }
    
    private void OnMouseUp()
    {
        _moving = false;
        _mousePos = null;
    }
}