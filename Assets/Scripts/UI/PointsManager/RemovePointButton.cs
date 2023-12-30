using System;
using UnityEngine;
using UnityEngine.UI;

public class RemovePointButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        if (!Settings.InPlayground)
            gameObject.SetActive(false);
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }
    
    private void Start()
    {
        PointManager.Instance.OnPointAdded += OnPointAdded;
        _button.interactable = !PointManager.Instance.IsMinimum();
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

    private void OnDestroy()
    {
        if (PointManager.Instance == null) return;
        PointManager.Instance.OnPointAdded -= OnPointAdded;
    }
}
