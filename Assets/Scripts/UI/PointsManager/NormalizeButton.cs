using UnityEngine;
using UnityEngine.UI;

public class NormalizeButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        if (!Settings.InPlayground)
            gameObject.SetActive(false);
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }
    
    private void OnClick()
    {
        PointManager.Instance.NormalizeWeights();
    }
}
