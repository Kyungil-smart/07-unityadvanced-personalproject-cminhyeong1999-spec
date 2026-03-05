using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float gameTime;
    private float progress;
    private StageLevel _level = StageLevel.Level1;
    public bool IsGameStarted = false;
    private readonly float _maxGameTime = 1200f;         // 20분 제한
    private readonly float _LevelChangeGameTime = 240f; // 구간을 5등분
    
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
        ManagementInit<MonsterPoolManager>();
        ManagementInit<BulletPoolManager>();
    }

    private void ManagementInit<T>() where T : Component
    {
        if (FindAnyObjectByType<T>() != null) return;
        
        var component = new GameObject(typeof(T).Name);
        component.AddComponent<T>();
        DontDestroyOnLoad(component);
    }

    private void Update()
    {
        if (!IsGameStarted)  return;
        gameTime += Time.deltaTime; // 총 경과 시간을 표시하기 위함
        progress += Time.deltaTime; // 일정 시간이 지나면 스테이지의 몬스터를 변경하기 위한 시간
        LevelChange();
    }

    private void LevelChange()
    {
        if (progress >= _LevelChangeGameTime)   // 진행 정도가 (제한시간 / 5) 한 간격 이상일 경우
        {
            _level++;
            // 레벨을 증가시킨 후 구독자에게 알림
            EventManager.Instance.PublishOnLevelChanged(_level);
            // 진행 정도 초기화
            progress = 0f;
        }
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
