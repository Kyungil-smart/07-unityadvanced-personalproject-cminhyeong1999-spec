using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;
    
    private void OnEnable()
    {
        _button1.onClick.AddListener(NewSceneClicked);
        _button2.onClick.AddListener(GameQuit);
    }
    
    private void OnDisable()
    {
        _button1.onClick.RemoveListener(NewSceneClicked);
        _button2.onClick.RemoveListener(GameQuit);
    }

    public void NewSceneClicked()
    {
        SceneLoader.Instance.LoadScene("SampleScene");
    }
    
    public void GameQuit()
    {
        GameManager.Instance.EndGame();
    }
}
