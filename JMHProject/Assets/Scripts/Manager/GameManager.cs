using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        Init();
    }

    private void Init()
    {
        ManagementInit<SceneLoader>();
        ManagementInit<InputManager>();
        ManagementInit<EventManager>();
    }

    private void ManagementInit<T>() where T : Component
    {
        if (FindAnyObjectByType<T>() != null) return;
        
        var component = new GameObject(typeof(T).Name);
        component.AddComponent<T>();
        DontDestroyOnLoad(component);
    }

    public void EndGame()
    {
        // 
        #if UNITY_EDITOR
            Debug.Log("Game Quit");
        #else
            Application.Quit();
        #endif
    }
    
}
