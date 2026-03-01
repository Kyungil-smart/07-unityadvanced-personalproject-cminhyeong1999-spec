using UnityEngine;

public class CreatePlayer : MonoBehaviour
{
    private PlayerPresenter _presenter;
    
    void Start()
    {
        var view = GetComponent<PlayerView>();
        var model = new PlayerModel();
        
        _presenter = new PlayerPresenter(model, view);
        view.SetPresenter(_presenter);
        model.SetPresenter(_presenter);
        Debug.Log("Create Player");
    }
    
}
