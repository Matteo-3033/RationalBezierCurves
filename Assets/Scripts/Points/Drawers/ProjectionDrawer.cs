using System;
using UnityEngine;

public class ProjectionDrawer : MonoBehaviour
{
    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Vector3[] _vertices;
    private int[] _triangles;
    
    private void InitMesh()
    {
        InitMeshVertices();

        _triangles = new int[(_vertices.Length - 2) * 3];

        for (var i = 0; i < _vertices.Length - 2; i++)
        {
            var t = i * 3;
            _triangles[t] = i;
            if (i % 2 == 0)
            {
                _triangles[t + 1] = i + 1;
                _triangles[t + 2] = i + 2;
            }
            else
            {
                _triangles[t + 1] = i + 2;
                _triangles[t + 2] = i + 1;
            }
        }

        _mesh.triangles = _triangles;
    }
    
    private void InitMeshVertices()
    {
        var curve = PointManager.Instance.GetCurve();
     
        _vertices = new Vector3[2000];
        for (var i = 0; i < _vertices.Length; i += 2)
        {
            var t = (float) i / (_vertices.Length - 1);
            var p = curve.GetPoint(t);
            _vertices[i] = p;
            _vertices[i + 1] = p / p.y;
        }
        
        _mesh.vertices = _vertices;
    }

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        
        _mesh = new Mesh();
        _meshFilter.mesh = _mesh; }

    private void Start()
    {
        PointManager.Instance.OnPointAdded += OnPointAdded;
        PointManager.Instance.OnLastPointRemoved += OnPointRemoved;
        Settings.OnShowProjectionMeshChanged += OnShowProjectionMeshChanged;
        
        foreach (var p in PointManager.Instance)
            p.OnPointChanged += OnPointChanged;
        
        InitMesh();
    }

    private void OnShowProjectionMeshChanged(object sender, Settings.OnShowProjectionMeshArgs e)
    {
        _meshRenderer.enabled = e.Show;
    }

    private void OnPointAdded(object sender, PointManager.OnPointArgs e)
    {
        e.Point.OnPointChanged += OnPointChanged;
        InitMeshVertices();
    }

    private void OnPointRemoved(object sender, EventArgs e)
    {
        InitMeshVertices();
    }

    private void OnPointChanged(object sender, PointManager.OnPointArgs e)
    {
        InitMeshVertices();
    }
}