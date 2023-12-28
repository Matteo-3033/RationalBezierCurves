using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class PointCoords : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointName;
    [SerializeField] private TMP_InputField xInputField;
    [SerializeField] private TMP_InputField yInputField;
    [SerializeField] private TMP_InputField wInputField;
    
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
        wInputField.onEndEdit.AddListener(OnWeightChanged);
    }

    private void Start()
    {
        if (!Settings.InPlayground)
        {
            xInputField.interactable = false;
            yInputField.interactable = false;
            if (!_point.Name.Equals("B") || Settings.Preset == PointManager.Preset.Circonferenza)
                wInputField.interactable = false;
        }
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

        pointName.text = _point.Name + ":"; 
        
        UpdateFields();
    }

    private void UpdateFields()
    {
        xInputField.text = FormatCoord(_point.UPosition.x);
        yInputField.text = FormatCoord(_point.UPosition.y);
        wInputField.text = FormatCoord(_point.Weight);
    }

    private void OnPointChanged(object sender, EventArgs e)
    {
        UpdateFields();
    }

    private static string FormatCoord(float coord)
    {
        return coord.ToString(CultureInfo.CurrentCulture);
    }
}
