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
    public event Action<int> OnHpIncreased;
    public event Action<int> OnHpDecreased;
    public event Action<float> OnMoveSpeedChanged;
    
    // 이벤트 출판 함수
    public void PublishOnLevelChanged(StageLevel level) => LevelChanged?.Invoke(level);
    public void PublishOnDeath() => OnDeath?.Invoke();
    public void PublishOnHpIncreased(int hp) => OnHpIncreased?.Invoke(hp);
    public void PublishOnHpDecreased(int hp) => OnHpDecreased?.Invoke(hp);
    public void PublishOnMoveSpeedChanged(float moveSpeed) => OnMoveSpeedChanged?.Invoke(moveSpeed);
    
    
    
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
