public class PlayerPresenter
{
    private PlayerModel _playermodel;
    private PlayerView _playerview;
    
    public PlayerPresenter(PlayerModel model, PlayerView view)
    {
        _playermodel = model;
        _playerview = view;
        _playerview.SetPresenter(this);
        
    }

    
    void Update()
    {
        
    }
}
