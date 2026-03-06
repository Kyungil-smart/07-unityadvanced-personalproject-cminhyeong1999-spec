using System;
using UnityEngine;

public class PlayerModel
{
    private PlayerPresenter _presenter;
    private PlayerData _data;
    
    // 스텟
    private string _name;
    private int _hp;
    private int _heal;
    private int _exp;
    private int _enforcePower;
    private float _maxSpeed;
    private int level;
    
    public int _maxHp;
    public int _maxExp;
    
    
    public float _moveSpeed { get; private set; }
    public Vector2 Pos { get; set; }
    public Vector2 CurrentVelocity { get; set; }
    
    public PlayerModel(PlayerData _setData)
    {
        _data = _setData;
        
        _name = _setData.baseName;
        _maxHp = _setData.maxHp;
        _hp = _maxHp;
        _moveSpeed = _setData.baseMoveSpeed;
        _heal = _setData.heal;
        _maxSpeed = _setData.maxSpeedLimit;
        _exp = 0;
        level = 1;
        _maxExp = _setData.startExp;
        _enforcePower = _setData.startEnforcePower;
        
        Pos = Vector2.zero;
    }
    
    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        _hp -= damage;
        
        if (_hp <= 0)
        {
            EventManager.Instance.PublishOnDeath();
        }
    }
    
    public void HealHp(int heal)
    {
        if (heal <= 0) return;
        
        // 받은 힐량이 _maxHp 이상이면 _maxHp, 이하이면 _hp + heal
        _hp = Math.Min(_maxHp, _hp + heal);
    }

    public int GetDamage(int baseDamage)
    {
        // 레벨당 데미지 2씩 고정 증가
        // 무기 데미지에 추가 계수 식을 적용
        return baseDamage + (_enforcePower * 2);
    }

    public int GetLevel()
    {
        return level;
    }

    // 경험치를 추가한 후 그 수치가 최대치를 넘기면 true, 아니면 false를 반환
    // presenter가 이를 근거로 레벨업 이벤트를 발행할지 말지를 결정
    public bool IsLevelUp(int exp)
    {
        _exp += exp;
        
        if (_exp >= _maxExp)
        {
            _exp = 0;
            LevelUpInit();
            return true;
        }
        
        return false;
    }

    // 레벨업 시 추가할 데이터
    private void LevelUpInit()
    { 
        _maxHp += _data.hpGainPerLevel;
        _hp = _maxHp;   // 레벨업 시 갱신된 최대체력만큼 hp 리셋
        _moveSpeed += _data.addSpeedLevel;
        _enforcePower += _data.enforcePower;
        level++;
        _exp = 0;
        
        _maxExp = _data.GetMaxExp(level);
    }
    
    public void ChangeSpeed(float speed)
    {
        float newSpeed = _moveSpeed + speed;
        
        if(newSpeed > _maxSpeed) newSpeed = _maxSpeed;
        
        if (Mathf.Approximately(_moveSpeed, newSpeed)) return;
        
        _moveSpeed = newSpeed;
        EventManager.Instance.PublishOnMoveSpeedChanged(_moveSpeed);
    }
    
    public void SetPresenter(PlayerPresenter presenter)
    {
        _presenter = presenter;
    }
    
    
}
