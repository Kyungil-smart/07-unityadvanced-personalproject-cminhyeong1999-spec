using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    private NewInputSystem _inputActions;
    
    // 이벤트
    public event Action<Vector2> Walk;
    public event Action<Vector2> Click;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // 코드로 new input system 추가
        _inputActions = new NewInputSystem();
        _inputActions.Enable();
        
        // [KeyBoard - Input] 액션 구독
        _inputActions.KeyBoard.Input.performed += ctx => Walk?.Invoke(ctx.ReadValue<Vector2>());
        _inputActions.KeyBoard.Input.canceled += ctx => Walk?.Invoke(Vector2.zero); // 키 뗐을 때 0 전달

        // [Click - Click] 액션 구독
        _inputActions.Click.Click.performed += ctx => Click?.Invoke(ctx.ReadValue<Vector2>());
    }

    public void OnInput(InputValue input)
    {
        Debug.Log($"OnInput");
        Vector2 pos = input.Get<Vector2>();
        Walk?.Invoke(pos);
    }

    public void OnClick(InputValue click)
    {
        Debug.Log($"OnClick");
        Vector2 mousePos = click.Get<Vector2>();
        Click?.Invoke(mousePos);
    }
    
    public void Terminate()
    {
        _inputActions?.Disable();
        _inputActions?.Dispose();
    }
}
