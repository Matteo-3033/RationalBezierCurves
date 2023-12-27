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
    
    private void OnClick()
    {
        PointManager.Instance.NormalizeWeights();
    }
}
