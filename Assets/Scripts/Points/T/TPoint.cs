using UnityEngine;

public class TPoint : MonoBehaviour
{
    private void Start()
    {
        TSelector.OnSelectedTChanged += OnSelectedTChanged;
    }

    private void OnSelectedTChanged(object sender, TSelector.OnSelectedTChangedArgs e)
    {
        transform.position = e.P;
    }
}