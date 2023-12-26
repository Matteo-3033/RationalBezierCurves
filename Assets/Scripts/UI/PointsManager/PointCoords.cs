using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class PointCoords : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointName;
    [SerializeField] private TMP_InputField xInputField;
    [SerializeField] private TMP_InputField yInputField;
    
    private WeightedPoint _point;
    
    public WeightedPoint Point
    {
        get => _point;
        set => SetPoint(value);
    }

    private void Awake()
    {
        xInputField.onEndEdit.AddListener(OnXChanged);
        yInputField.onEndEdit.AddListener(OnYChanged);
    }

    private void Start()
    {
        DeCasteljauDrawer.OnDrawingChanged += OnDrawingChanged;
    }

    private void OnDrawingChanged(object sender, DeCasteljauDrawer.OnDrawingChangedArgs e)
    {
        xInputField.interactable = !e.IsDrawing;
        yInputField.interactable = !e.IsDrawing;
    }

    private void OnXChanged(string xStr)
    {
        if (float.TryParse(xStr, out var x))
            _point.UPosition = new Vector2(x, _point.UPosition.y);
    }
    
    private void OnYChanged(string yStr)
    {
        if (float.TryParse(yStr, out var y))
            _point.UPosition = new Vector2(_point.UPosition.x, y);
    }

    private void SetPoint(WeightedPoint p)
    {
        if (_point != null)
            throw new UnityException("Point is already set");
        
        _point = p;
        _point.OnPointChanged += OnPointChanged;

        pointName.text = _point.Name + ":"; 
        
        UpdateFields();
    }

    private void UpdateFields()
    {
        xInputField.text = FormatCoord(_point.UPosition.x);
        yInputField.text = FormatCoord(_point.UPosition.y);
    }

    private void OnPointChanged(object sender, PointManager.OnPointArgs e)
    {
        UpdateFields();
    }

    private static string FormatCoord(float coord)
    {
        var str = coord.ToString(CultureInfo.CurrentCulture);
        return str != "0" ? str[1..] : str;
    }
}
