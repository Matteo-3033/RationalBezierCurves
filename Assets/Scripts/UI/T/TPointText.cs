using System;
using TMPro;
using UnityEngine;

public class TPointText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        TSelector.OnSelectedTChanged += OnSelectedTChanged;
    }

    private void OnSelectedTChanged(object sender, TSelector.OnSelectedTChangedArgs e)
    {
        _text.text = $"t = {e.T:F2} \u2192 ({e.P.z:F2}, {e.P.x:F2})";
    }
}
