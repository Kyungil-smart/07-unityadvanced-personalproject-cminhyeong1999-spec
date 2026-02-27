using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_velocity.x, _velocity.y);
        
        if(_velocity.x != 0)
            transform.localScale = new Vector2(-1, 1);
    }
}
