using UnityEngine;
using UnityEngine.UI;

public class DeCasteljauToggle : MonoBehaviour
{
    public void Awake()
    {
        var toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChanged);
        toggle.isOn = false;
    }

    private void OnValueChanged(bool value)
    {
	    Settings.ShowDeCasteljau = value;
    }
}
