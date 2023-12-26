using TMPro;
using UnityEngine;

public class WeightedPointName: MonoBehaviour
{
    [SerializeField] private WeightedPoint point;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _text.text = point.Name;
    }
}