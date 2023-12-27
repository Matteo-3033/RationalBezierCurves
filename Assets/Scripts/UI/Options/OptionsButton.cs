using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private bool close;
    
    private void Awake()
    {
        var button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        optionsMenu.SetActive(!close);
    }
}
