using System;
using TMPro;
using UnityEngine;

public class CurveDegree : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private string _baseText;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _baseText = _text.text;
    }

    private void Start()
    {
        PointManager.Instance.OnPointAdded += OnCurveChanged;
        PointManager.Instance.OnLastPointRemoved += OnCurveChanged;
        UpdateDegree();
    }

    private void OnCurveChanged(object sender, EventArgs e)
    {
        UpdateDegree();
    }

    private void UpdateDegree()
    {
        _text.text = _baseText + $"{PointManager.Instance.Count - 1}";
    }
}
