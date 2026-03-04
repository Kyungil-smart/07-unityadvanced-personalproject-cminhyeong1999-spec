using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float speed = 4f;
    private Vector2 target;

    private bool _isDead;

    private float _originalSpeed;
    private Rigidbody2D _rig;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        
    }

    // 활성화 되었을 경우
    private void OnEnable()
    {
        _isDead = false;
        // Enemy를 임시가속 한 후에 원래 스피드로 복구하기 위한 변수
        _originalSpeed = speed;
        if(_rig != null) _rig.linearVelocity = Vector2.zero;
    }
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_isDead) return;
        
        target = PlayerPresenter.Player.GetPos();
        Vector2 direction = target - _rig.position;
        
        // Enemy가 20 x 20 타일안의 두 거리의 최대값인 20*sqrt(2)를 벗어나면
        // 일시적으로 speed를 타일 내로 아주 빠르게 이동시키기 위함
        if (direction.magnitude > 29f)
        {
            speed = _originalSpeed + 80f;
        }
        else if (direction.magnitude <= 20f)    // 일정거리 안으로 들어오면 속도를 다시 원상복구
        {
            speed = _originalSpeed;
        }
        Vector2 nextVec = speed * Time.fixedDeltaTime * direction.normalized;
        _rig.MovePosition(_rig.position + nextVec);
        _rig.linearVelocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (_isDead) return;
        
        if (target.x > _rig.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (target.x < _rig.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
