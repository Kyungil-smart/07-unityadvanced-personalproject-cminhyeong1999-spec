using UnityEngine;

public class CreatePlayer : MonoBehaviour
{
    [SerializeField] private PlayerData _data;
    
    private PlayerPresenter _presenter;
    
    private void Start()
    {
        _presenter = Create();
    }

    public PlayerPresenter Create()
    {
        var view = GetComponent<PlayerView>();
        var model = new PlayerModel(_data);

        var tmp = new PlayerPresenter(model, view);
        //tmp.Init(model, view);
        view.SetPresenter(tmp);
        model.SetPresenter(tmp);
        _presenter = tmp;
        return tmp;
    }

    private void OnDisable()
    {
        _presenter.Terminate();
        _presenter = null;
    }

}
