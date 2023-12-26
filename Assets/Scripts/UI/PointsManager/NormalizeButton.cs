using UnityEngine;
using UnityEngine.UI;

public class NormalizeButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        DeCasteljauDrawer.OnDrawingChanged += OnDrawingChanged;
    }

    private void OnClick()
    {
        PointManager.Instance.NormalizeWeights();
    }
    
    private void OnDrawingChanged(object sender, DeCasteljauDrawer.OnDrawingChangedArgs e)
    {
        _button.interactable = !e.IsDrawing;
    }
}
