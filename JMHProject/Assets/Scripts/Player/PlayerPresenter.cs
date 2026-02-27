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
        
        _playermodel.SetPos(direction);
        _playerview.SetVelocity(direction);
    }
    
    public void Terminate()
    {
        InputManager.Instance.Walk -= Walk;
    }
}
