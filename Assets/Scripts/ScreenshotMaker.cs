using System;
using UnityEngine;

public class ScreenshotMaker : MonoBehaviour
{
    [SerializeField] public KeyCode screenShotButton;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(screenShotButton)) return;
        ScreenCapture.CaptureScreenshot("./Screenshots/" + Time.time + ".png");
        Debug.Log("A screenshot was taken!");
    }
}
