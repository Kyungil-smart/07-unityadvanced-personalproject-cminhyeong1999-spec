using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public Vector2 PlayerPosition { get; private set; }
    public Vector2 MousePosition { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnInput(InputValue input)
    {
        PlayerPosition = input.Get<Vector2>();
    }

    public void OnClick(InputValue click)
    {
        MousePosition = click.Get<Vector2>();
    }
}
