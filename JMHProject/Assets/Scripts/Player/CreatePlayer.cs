using UnityEngine;

public class CreatePlayer : MonoBehaviour
{
    private PlayerPresenter _presenter;
    
    private void Start()
    {
        _presenter = Create();
    }

    public PlayerPresenter Create()
    {
        var view = GetComponent<PlayerView>();
        var model = new PlayerModel();
        
        var tmp = new PlayerPresenter(model, view);
        view.SetPresenter(tmp);
        model.SetPresenter(tmp);
        return tmp;
    }
}
