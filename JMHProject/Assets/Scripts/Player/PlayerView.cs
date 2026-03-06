using System.Collections;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    // 무적시 깜빡임 효과를 위한 변수
    [SerializeField] private SpriteRenderer sprite;
    
    private PlayerPresenter _presenter;
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    private bool _isInvincible = false; // 무적 상태 체크
    [SerializeField] private float invincibilityTime = 1f; // 무적 시간
    
    public Transform visualTransform; // 스프라이트 이미지 반전용
    public Animator ani;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        ani = GetComponentInChildren<Animator>();
    }
    
    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _velocity;
    }

    public void SetPresenter(PlayerPresenter presenter)
    {
        _presenter = presenter;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }
    
    public void SetFlipped(Vector2 velocity)
    {
        if (velocity.x < 0)
        {
            visualTransform.localScale = new Vector3(-1, 1, 1);
        }
        else if (velocity.x > 0)
        {
            visualTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    public Vector2 GetPos()
    {
        var x = transform.position.x;
        var y = transform.position.y;
        return new Vector2(x, y);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        // 무적상태가 아니고 충돌한 적이 있으면 데미지 판정
        if (collision.CompareTag("Enemy") && !_isInvincible)
        {
            int takeDamage = collision.GetComponent<Enemy>().Attack();
            _presenter.TakeDamage(takeDamage);
            EventManager.Instance.PublishOnHpDecreased(takeDamage);
            // 무적 판정 시작
            StartCoroutine(Invincibility());
        }
        
    }
    
    private IEnumerator Invincibility()
    {
        _isInvincible = true;
        

        yield return new WaitForSeconds(invincibilityTime);

        _isInvincible = false;
    }

}
