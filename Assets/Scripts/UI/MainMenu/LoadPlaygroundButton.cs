using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadPlaygroundButton : MonoBehaviour
{
    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SceneManager.LoadScene("PlaygroundScene");
    }
}
