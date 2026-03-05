using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerPresenter _presenter;
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    
    public Transform visualTransform; // 스프라이트 이미지 반전용
    public Animator ani;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        ani = GetComponentInChildren<Animator>();
    }
    
    /*
    private void Update()
    {
        // 매 프레임 Presenter에게 물리 계산 요청 (중력 등)
        _presenter?.UpdatePhysics(Time.deltaTime);
    }
    */
    
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
    
    /*
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_rigidbody.position + Vector2.down, 0.2f,LayerMask.GetMask("Ground"));
    }
    */
}
