using System;
using UnityEngine;
using UnityEngine.UI;

public class RemovePointButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        _button.interactable = false;
    }
    
    private void Start()
    {
        PointManager.Instance.OnPointAdded += OnPointAdded;
    }
    
    private void OnPointAdded(object sender, PointManager.OnPointArgs args)
    {
        _button.interactable = true;
    }
    
    private void OnClick()
    {
        PointManager.Instance.RemovePoint();
        _button.interactable = !PointManager.Instance.IsMinimum();
    }
}
