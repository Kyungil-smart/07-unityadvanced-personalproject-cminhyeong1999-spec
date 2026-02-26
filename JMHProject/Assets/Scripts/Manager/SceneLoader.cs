using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
    
    public void LoadScene(string sceneName)
    {
        //todo : 로딩씬을 보여주는 중에 맵 데이터나 몬스터 데이터 등을 정상적으로 불러야함
        //       그걸 굳이 씬 상에서 보여주지 않기 위해 로딩창을 따로 만들어서 이걸 보여줄 예정
        SceneManager.LoadScene(sceneName);
    }
}
