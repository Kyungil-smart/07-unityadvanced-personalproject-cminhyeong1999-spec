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
        // 추가 후 활성화
        _inputActions.Enable();
        
        // [KeyBoard - Input] 액션 구독
        // ctx.ReadValue<Vector2>()가 입력받은 값을 Vector2 형태로 다시 읽어줌, 이 값을 구독한 구독자에게 뿌림
        _inputActions.KeyBoard.Input.performed += ctx => Walk?.Invoke(ctx.ReadValue<Vector2>());
        // 키 뗐을 때 0 전달
        _inputActions.KeyBoard.Input.canceled += ctx => Walk?.Invoke(Vector2.zero);

        // [Click - Click] 액션 구독
        _inputActions.Click.Click.performed += ctx => Click?.Invoke(ctx.ReadValue<Vector2>());
    }
    
    public void Terminate()
    {
        _inputActions?.Disable();
        _inputActions?.Dispose();
    }
}
