using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    
    public static InputManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        _playerInput = new PlayerInput();
        _playerInput.Camera.Enable();
    }

    public float GetCameraRotation()
    {
        return _playerInput.Camera.Movement.ReadValue<Vector2>().normalized.x;
    }
    
    public float GetCameraDistance()
    {
        return _playerInput.Camera.Movement.ReadValue<Vector2>().normalized.y;
    }

    private void OnDestroy()
    {
        _playerInput.Camera.Disable();
    }
}
