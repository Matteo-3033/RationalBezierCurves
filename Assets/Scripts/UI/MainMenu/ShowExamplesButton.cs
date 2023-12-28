using UnityEngine;
using UnityEngine.UI;

public class ShowExamplesButton : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject examplesMenu;
    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        examplesMenu.SetActive(false);
    }

    private void OnClick()
    {
        mainMenu.SetActive(false);
        examplesMenu.SetActive(true);
    }
}
