using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadExamplePlaygroundButton : MonoBehaviour
{
    [SerializeField] private PointManager.Preset preset;
    
    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Settings.Preset = preset;
        SceneManager.LoadScene("PlaygroundScene");
    }
}
