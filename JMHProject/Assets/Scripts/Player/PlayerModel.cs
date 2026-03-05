using System;
using UnityEngine;

public class PlayerModel
{
    private PlayerPresenter _presenter;
    
    // 스텟
    private string _name;
    private int _hp;
    private int _maxHp;
    private int _exp;
    private readonly float _maxSpeed = 140f;
    
    public float _moveSpeed { get; private set; }
    public Vector2 Pos { get; set; }
    public Vector2 CurrentVelocity { get; set; }
    public Transform VisualPoint;
    

    public PlayerModel()
    {
        _name = "PLAYER";
        _maxHp = 100;
        _hp =   _maxHp;
        _moveSpeed = 7f;
        Pos = Vector2.zero;
        EventManager.Instance.OnHpDecreased += TakeDamage;
        EventManager.Instance.OnHpIncreased += HealHp;
    }
    
    public PlayerModel(string name)
    {
        _name = name;
        _maxHp = 100;
        _hp =   _maxHp;
        _moveSpeed = 7f;
        Pos = Vector2.zero;
        EventManager.Instance.OnHpDecreased += TakeDamage;
        EventManager.Instance.OnHpIncreased += HealHp;
    }
    
    private void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        // 받은 데미지가 0 이하로 떨어지면 0, 0 이상이면 _hp - damage
        _hp = Math.Max(0, _hp - damage);
        
        if (_hp <= 0)
        {
            EventManager.Instance.PublishOnDeath();
        }
    }
    
    private void HealHp(int heal)
    {
        if (heal <= 0) return;
        
        // 받은 힐량이 _maxHp 이상이면 _maxHp, 이하이면 _hp + heal
        _hp = Math.Min(_maxHp, _hp + heal);
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
    
    public void Terminate()
    {
        EventManager.Instance.OnHpDecreased -= TakeDamage;
        EventManager.Instance.OnHpIncreased -= HealHp;
    }
}
