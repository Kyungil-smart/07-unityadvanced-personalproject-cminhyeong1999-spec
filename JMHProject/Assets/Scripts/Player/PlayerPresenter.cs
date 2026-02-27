using UnityEngine;

public class PlayerPresenter
{
    private PlayerModel _playermodel;
    private PlayerView _playerview;
    
    public PlayerPresenter(PlayerModel model, PlayerView view)
    {
        _playermodel = model;
        _playerview = view;

        InputManager.Instance.Walk += Walk;
    }

    private void Walk(Vector2 input)
    {
        float speed = _playermodel._moveSpeed;
        Vector2 direction = new Vector2(input.x, input.y) * speed;
        
        _playermodel.SetPos(direction); // 위치값 저장용
        _playerview.SetVelocity(direction); // view 이미지 갱신용
        _playerview.SetFlipped(direction);  // 이미지 좌우 반전
    }
    
    public void Terminate()
    {
        InputManager.Instance.Walk -= Walk;
    }
}
