using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    
    private IObjectPool<GameObject> _pool;
    private GameObject _prefab;
    private int _maxPoolSize;
    private int _minPoolSize;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        Init();
    }

    private void Init()
    {
        _prefab = Resources.Load<GameObject>("Monster1");
        _minPoolSize = 40;
        _maxPoolSize = 300;
        
        // pool = new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease,
        // actionOnDestroy, collectionCheck, defaultCapacity, maxSize); 형식으로
        // 유니티에서 지원하는 ObjectPool을 사용할 수 있음
        // createFunc: 오브젝트 생성 함수 (Func)
        // actionOnGet: 풀에서 오브젝트를 가져오는 함수 (Action)
        // actionOnRelease: 오브젝트를 비활성화할 때 호출하는 함수 (Action)
        // actionOnDestroy: 오브젝트 파괴 함수 (Action)
        // collectionCheck: 중복 반환 체크 (bool)
        // defaultCapacity: 처음에 미리 생성하는 오브젝트 갯수 (int)
        // maxSize: 저장할 오브젝트의 최대 갯수 (int)
        _pool = new ObjectPool<GameObject>
            (CreateObject, 
            ActivatePoolObject, 
            DisablePoolObject , 
            DestroyPoolObject, 
            true, 
            _minPoolSize, _maxPoolSize);
    }
    
    private GameObject CreateObject() // 오브젝트 생성
    {
        return Instantiate(_prefab);
    }

    private void ActivatePoolObject(GameObject obj) // 오브젝트 활성화
    {
        obj.SetActive(true);
    }

    private void DisablePoolObject(GameObject obj) // 오브젝트 비활성화
    {
        obj.SetActive(false);
    }

    private void DestroyPoolObject(GameObject obj) // 오브젝트 삭제
    {
        Destroy(obj);
    }
    
    public GameObject GetObject()
    {
        return _pool.Get();
    }

    public void ReleaseObject(GameObject obj)
    {
        _pool.Release(obj);
    }
}
