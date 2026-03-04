using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    
    private IObjectPool<GameObject> _pool;
    private List<GameObject> _monsterPrefabs = new List<GameObject>();
    private GameObject _summonMonster1;
    private GameObject _summonMonster2;
    private int _maxPoolSize;
    private int _minPoolSize;

    public int summonIndex = 0;
    
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
        _monsterPrefabs = Resources.LoadAll<GameObject>("Monsters").ToList();
        SetSummonTypes(summonIndex, summonIndex + 1);
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
        // 0.0 ~ 1.0 까지의 랜덤 값 중 0.65를 넘기지 못하면 _summonMonster1, 아니면 _summonMonster2 선택
        GameObject nowSummon = (Random.value < 0.65f) ? _summonMonster1 : _summonMonster2;

        if (nowSummon == null) return null;

        return Instantiate(nowSummon);
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
    
    // 소환 할 두 종류의 몬스터를 지정하기 위한 메서드 
    public void SetSummonTypes(int index1, int index2)
    {
        if (_monsterPrefabs.Count == 0 || index1 > _monsterPrefabs.Count || index2 > _monsterPrefabs.Count) return;

        // 인덱스가 리스트 범위를 넘지 않게 Clamp 처리
        int i1 = Mathf.Clamp(index1, 0, _monsterPrefabs.Count - 1);
        int i2 = Mathf.Clamp(index2, 0, _monsterPrefabs.Count - 1);

        _summonMonster1 = _monsterPrefabs[i1];
        _summonMonster2 = _monsterPrefabs[i2];
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
