using System;
using UnityEngine;

public class PlayerModel
{
    
    // 스텟
    private string _name;
    private int _hp;
    private int _str;
    private int _dex;
    private int _int;
    private int _luk;
    private int _exp;
    private Vector2 _pos;
    public float _moveSpeed { get; private set; }
    private float _jumpForce;
    private float _maxSpeed = 140f;
    
    // 이벤트
    public event Action OnDeath;
    public event Action<int> OnHpChanged;
    public event Action<int> OnStrChanged;
    public event Action<int> OnDexChanged;
    public event Action<int> OnIntChanged;
    public event Action<int> OnLukChanged;
    public event Action<float> OnMoveSpeedChanged;

    public PlayerModel()
    {
        _name = "PLAYER";
        _str = 12;
        _dex = 4;
        _int = 4;
        _luk = 4;
        _hp = 100;
        _moveSpeed = 10f;
        _jumpForce = 10f;
    }
    
    public PlayerModel(string name, int str, int dex, int Int, int luk)
    {
        _name = name;
        _str = str;
        _dex = dex;
        _int = Int;
        _luk = luk;
        _hp = 100;
    }
    
    public void TakeDamage(int Damage)
    {
        if (Damage <= 0) return;

        _hp = Math.Max(0, _hp - Damage);
        OnHpChanged?.Invoke(_hp);
        
        if (_hp <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void SetPos(Vector2 pos)
    {
        _pos = pos;
    }
    
    public void ChangeSpeed(float speed)
    {
        float newSpeed = _moveSpeed + speed;
        
        if(newSpeed > _maxSpeed) newSpeed = _maxSpeed;
        
        if (Mathf.Approximately(_moveSpeed, newSpeed)) return;
        
        _moveSpeed = newSpeed;
        OnMoveSpeedChanged?.Invoke(_moveSpeed);
    }
}
