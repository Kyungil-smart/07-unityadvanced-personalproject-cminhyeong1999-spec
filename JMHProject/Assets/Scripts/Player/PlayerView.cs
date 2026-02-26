using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerPresenter _presenter;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetPresenter(PlayerPresenter presenter)
    {
        _presenter = presenter;
    }
}
