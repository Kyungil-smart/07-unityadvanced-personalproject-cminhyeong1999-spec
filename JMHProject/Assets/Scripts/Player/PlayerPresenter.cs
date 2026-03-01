using UnityEngine;

public class PlayerPresenter
{
    private PlayerModel _playermodel;
    private PlayerView _playerview;
    
    private bool _isJumping = false;
    private bool _isWalking = false;
    
    public PlayerPresenter(PlayerModel model, PlayerView view)
    {
        _playermodel = model;
        _playerview = view;

        InputManager.Instance.Walk += Walk;
        InputManager.Instance.Jump += Jump;
    }

    private void Walk(Vector2 input)
    {
        if(input == Vector2.zero) _isWalking = false;
        else _isWalking = true;
        
        float speed = _playermodel._moveSpeed;
        Vector2 currentVel = _playermodel.CurrentVelocity;

        currentVel.x = input.x * speed;
        _playermodel.CurrentVelocity = currentVel;
        
        _playerview.SetFlipped(currentVel);  // 이미지 좌우 반전
        
        Debug.Log(_isWalking);
    }

    private void Jump()
    {
        // 지면에 있을 때만 점프 가능
        if (_playermodel.IsGrounded)
        {
            Vector2 currentVel = _playermodel.CurrentVelocity;
            currentVel.y = _playermodel._jumpForce; // 점프 힘 적용
            _playermodel.CurrentVelocity = currentVel;
        }
    }

    // 지면 체크, 임의 중력 적용 등을 매 프레임 체크하기 위한 메서드
    public void UpdatePhysics(float deltaTime)
    {
        // 지면 체크 후 model에 저장
        _playermodel.IsGrounded = _playerview.IsGrounded();
        
        Vector2 velocity = _playermodel.CurrentVelocity;

        if (_playermodel.IsGrounded)
        {
            if (velocity.y < 0) velocity.y = 0; // 바닥에 있으면 추락 속도 제거
        }
        else
        {
            // 공중이라면 중력 적용
            velocity.y += _playermodel.Gravity * deltaTime;
        }

        // 결과값을 모델에 저장하고 뷰에 전달
        _playermodel.CurrentVelocity = velocity;
        _playerview.SetVelocity(velocity);
    }
    
    public void Terminate()
    {
        InputManager.Instance.Walk -= Walk;
        InputManager.Instance.Jump -= Jump;
    }
}
