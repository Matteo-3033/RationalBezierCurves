using System;
using UnityEngine;
using UnityEngine.UI;

public class AddPointButton : MonoBehaviour
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
        PointManager.Instance.OnLastPointRemoved += OnLastPointRemoved;
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

    private void OnDestroy()
    {
        if (PointManager.Instance == null) return;
        PointManager.Instance.OnLastPointRemoved -= OnLastPointRemoved;
    }
}
