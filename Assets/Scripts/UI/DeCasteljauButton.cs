using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeCasteljauButton : MonoBehaviour
{
    [SerializeField] private Slider tSlider;
    [SerializeField] private DeCasteljauDrawer drawer;
    private TextMeshProUGUI _text;
    private Button _button;

    public void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        PointManager.Instance.OnPointAdded += OnPointChanged;
        PointManager.Instance.OnLastPointRemoved += OnPointChanged;
        OnPointChanged(null, null);
    }

    private void OnPointChanged(object sender, EventArgs e)
    {
        _button.interactable = PointManager.Instance.Count > 2;
    }

    private void OnClick()
    {
        if (DeCasteljauDrawer.IsDrawing)
            drawer.Next();
        else drawer.Init(tSlider.value);

        _text.text = DeCasteljauDrawer.IsDrawing ? "Avanti" : "De Casteljau";
    }
}
