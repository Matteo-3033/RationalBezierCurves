using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class PointWeight : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    
    private WeightedPoint _point;
    
    public WeightedPoint Point
    {
        get => _point;
        set => SetPoint(value);
    }

    private void Awake()
    {
        inputField.onEndEdit.AddListener(OnWeightChanged);
    }
    
    private void OnWeightChanged(string weightStr)
    {
        if (float.TryParse(weightStr, out var weight))
            _point.Weight = weight;
    }

    private void SetPoint(WeightedPoint p)
    {
        if (_point != null)
            throw new UnityException("Point is already set");
        
        _point = p;
        _point.OnPointChanged += OnPointChanged;
        
        UpdateFields();
    }

    private void UpdateFields()
    {
        inputField.text = FormatCoord(_point.Weight);
    }

    private void OnPointChanged(object sender, EventArgs e)
    {
        UpdateFields();
    }

    private static string FormatCoord(float coord)
    {
        var str = coord.ToString(CultureInfo.CurrentCulture);
        if (str[0] == '0') 
            str = str[1..];
        return str;
    }
    
}
