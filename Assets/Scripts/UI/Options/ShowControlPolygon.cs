using UnityEngine;
using UnityEngine.UI;

public class ShowControlPolygon : MonoBehaviour
{
    private void Awake()
    {
        var toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool value)
    {
        Settings.ShowControlPolygon = value;
    }
}
