using TMPro;
using UnityEngine;
using UnityEngine.UI;

// UI에 일일이 스크립트를 넣어서 각자가 수정해야하는 값을 수정할 예정
public class HUD : MonoBehaviour
{
    private int killcount = 0;
    
    public enum InfoType { Exp, Level, Kill, Health }
    public InfoType infoType;

    public Slider _slider;
    public TextMeshProUGUI _text;

    private void Awake()
    { 
        _slider = GetComponent<Slider>();
        _text = GetComponent<TextMeshProUGUI>();
    } 
    
    private void Start()
    {
        if (EventManager.Instance == null) return;
        
        InitUI(); // 시작 시 최대치 및 초기값 세팅

        // 타입별 이벤트 구독
        switch (infoType)
        {
            case InfoType.Exp:
                EventManager.Instance.OnExpIncreased += UpdateValue;
                EventManager.Instance.OnPlayerLevelUp += HandleLevelUp;
                break;
            case InfoType.Health:
                EventManager.Instance.OnHpIncreased += UpdateValue;
                EventManager.Instance.OnHpDecreased += UpdateValue;
                break;
            case InfoType.Level:
                EventManager.Instance.OnPlayerLevelUp += UpdateLevelText;
                break;
            case InfoType.Kill:
                EventManager.Instance.OnKilled += UpdateKillCount;
                break;
        }
    }

    private void InitUI()
    {
        if (PlayerPresenter.Player == null) return;

        switch (infoType)
        {
            case InfoType.Health:
                _slider.maxValue = PlayerPresenter.Player.GetMaxHp();
                _slider.value = _slider.maxValue;
                break;
            case InfoType.Exp:
                _slider.maxValue = PlayerPresenter.Player.GetMaxExp();
                _slider.value = 0;
                break;
            case InfoType.Level:
                UpdateLevelText();
                break;
            case InfoType.Kill:
                if (_text != null) _text.text = "0";
                break;
        }
    }

    // 슬라이더 및 일반 수치 텍스트 갱신
    private void UpdateValue(int value)
    {
        if (_slider != null) _slider.value += value;
        if (_text != null) _text.text = value.ToString();
    }

    // 킬 카운트 등 단순 텍스트 갱신
    private void UpdateText(int value)
    {
        if (_text != null) _text.text = value.ToString();
    }

    private void UpdateKillCount(int value)
    {
        if (_text != null)
        {
            killcount += value;
            _text.text = $"{killcount}";
        }
    }

    // 레벨 텍스트 갱신 (전역 변수에서 현재 레벨 참조)
    private void UpdateLevelText()
    {
        if (_text != null && PlayerPresenter.Player != null)
            _text.text = $"Lv.{PlayerPresenter.Player.GetCurrentLevel()}";
    }

    private void HandleLevelUp()
    {
        if (infoType == InfoType.Exp && _slider != null)
        {
            _slider.maxValue = PlayerPresenter.Player.GetMaxExp();
            _slider.value = 0;
        }
    }

    private void OnDisable()
    {
        if (EventManager.Instance == null) return;
        
        EventManager.Instance.OnExpIncreased -= UpdateValue;
        EventManager.Instance.OnHpIncreased -= UpdateValue;
        EventManager.Instance.OnHpDecreased -= UpdateValue;
        EventManager.Instance.OnKilled -= UpdateText;
        EventManager.Instance.OnPlayerLevelUp -= UpdateLevelText;
        EventManager.Instance.OnPlayerLevelUp -= HandleLevelUp;
    }
}