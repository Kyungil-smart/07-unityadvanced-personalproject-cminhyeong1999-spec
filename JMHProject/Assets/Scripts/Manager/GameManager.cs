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
        CreatePlayer();
    }

    private void ManagementInit<T>() where T : Component
    {
        if (FindAnyObjectByType<T>() != null) return;
        
        var component = new GameObject(typeof(T).Name);
        component.AddComponent<T>();
        DontDestroyOnLoad(component);
    }

    private void CreatePlayer()
    {
        PlayerModel _player = new PlayerModel("Player",4,4,4,4);
        PlayerView _pview = new PlayerView();
        PlayerPresenter _pp = new PlayerPresenter(_player, _pview);
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
