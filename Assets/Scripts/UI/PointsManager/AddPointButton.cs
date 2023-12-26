using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AddPointButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        PointManager.Instance.OnLastPointRemoved += OnLastPointRemoved;
        DeCasteljauDrawer.OnDrawingChanged += OnDrawingChanged;
    }
    
    private void OnLastPointRemoved(object sender, EventArgs args)
    {
        _button.interactable = true;
    }

    private void OnClick()
    {
        PointManager.Instance.NewPoint();
        _button.interactable = !PointManager.Instance.IsFull();
    }
    
    private void OnDrawingChanged(object sender, DeCasteljauDrawer.OnDrawingChangedArgs e)
    {
        _button.interactable = !e.IsDrawing;
    }
}
