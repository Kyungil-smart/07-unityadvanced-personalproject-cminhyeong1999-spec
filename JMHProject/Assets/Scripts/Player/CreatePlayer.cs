using UnityEngine;

public class CreatePlayer : MonoBehaviour
{
    private PlayerPresenter _presenter;
    
    void Start()
    {
        var view = GetComponent<PlayerView>();
        var model = new PlayerModel();
        
        _presenter = new PlayerPresenter(model, view);
        Debug.Log("Create Player");
    }
    
}
