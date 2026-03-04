using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private MonsterData _data;
    private Vector2 _target;

    private bool _isDead;
    private int _currentHealth;
    private float _currentSpeed;
    private Rigidbody2D _rig;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        
    }

    // 오브젝트 풀에서 생성 후 Init
    public void Init(MonsterData newData)
    {
        _data = newData;
        _isDead = false;
        _currentHealth = _data.maxHealth;
        _currentSpeed = _data.baseSpeed;
        if(_rig != null) _rig.linearVelocity = Vector2.zero;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_isDead) return;
        
        _target = PlayerPresenter.Player.GetPos();
        Vector2 direction = _target - _rig.position;
        
        // Enemy가 20 x 20 타일안의 두 거리의 최대값인 20*sqrt(2)를 벗어나면
        // 일시적으로 speed를 타일 내로 아주 빠르게 이동시키기 위함
        if (direction.magnitude > 29f)
        {
            _currentSpeed = _data.baseSpeed + 80f;
        }
        else if (direction.magnitude <= 20f)    // 일정거리 안으로 들어오면 속도를 다시 원상복구
        {
            _currentSpeed = _data.baseSpeed;
        }
        Vector2 nextVec = _currentSpeed * Time.fixedDeltaTime * direction.normalized;
        _rig.MovePosition(_rig.position + nextVec);
        _rig.linearVelocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (_isDead) return;
        
        if (_target.x > _rig.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_target.x < _rig.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
        }
        else if (collision.CompareTag("Weapon"))
        {
            
        }
    }
}
