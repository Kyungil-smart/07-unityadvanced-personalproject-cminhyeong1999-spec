using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameEvent
{
    LevelChanged,
    OnDeath,
    OnHpIncreased,
    OnMoveSpeedChanged
}

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    
    // 이벤트 목록
    public event Action<StageLevel> LevelChanged;
    public event Action OnDeath;
    public event Action OnPlayerLevelUp; 
    public event Action<int> OnHpIncreased;
    public event Action<int> OnHpDecreased;
    public event Action<int> OnExpIncreased;
    public event Action<int> OnKilled;
    public event Action<float> OnMoveSpeedChanged;
    
    // 이벤트 출판 함수
    public void PublishOnLevelChanged(StageLevel level) => LevelChanged?.Invoke(level);
    public void PublishOnDeath() => OnDeath?.Invoke();
    public void PublishOnPlayerLevelUp() => OnPlayerLevelUp?.Invoke();
    public void PublishOnHpIncreased(int hp) => OnHpIncreased?.Invoke(hp);
    public void PublishOnHpDecreased(int hp) => OnHpDecreased?.Invoke(hp);
    public void PublishOnExpIncreased(int exp) => OnExpIncreased?.Invoke(exp);
    public void PublishOnMoveSpeedChanged(float moveSpeed) => OnMoveSpeedChanged?.Invoke(moveSpeed);
    public void PublishOnKilled(int killCount) => OnKilled?.Invoke(killCount);
    
    
    
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

}
