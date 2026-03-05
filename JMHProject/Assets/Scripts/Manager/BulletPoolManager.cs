using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;

    // 총알 프리펩별로 꺼내 쓰기 위해 Dictionary 구조 사용
    private Dictionary<GameObject, Queue<GameObject>> _pools = new();
    private int _globalMaxBulletCount = 150; // 풀에 최대로 저장할 총알 개수
    public int _activeBulletCount = 0;       // 씬에 활성화 된 총알 갯수
    

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

    public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        // 해당 프리펩을 위한 풀이 없으면 새로 생성
        if (!_pools.ContainsKey(prefab))
        {
            _pools.Add(prefab, new Queue<GameObject>());
        }

        GameObject select = null;

        // 풀에 남은 총알이 있다면 꺼내기
        if (_pools[prefab].Count > 0)
        {
            select = _pools[prefab].Dequeue();
            select.transform.position = position;
            select.transform.rotation = rotation;
            select.SetActive(true);
            return select;
        }
        
        if (_activeBulletCount < _globalMaxBulletCount)
        {
            // 풀이 비었을 때 최대치보다 적으면 새로 생성
            select = Instantiate(prefab, position, rotation, transform);
            _activeBulletCount++;
            return select;
        }
        
        // 풀에도 없고 새로 만들 수도 없다면 null 리턴
        return null;
    }

    public void Release(GameObject prefab, GameObject bullet)
    {
        if (!bullet.activeSelf) return;
        
        // 사용이 끝난 총알은 다시 큐에 넣고 비활성화
        bullet.SetActive(false);
        _pools[prefab].Enqueue(bullet);
        _activeBulletCount--; // 비활성화 되어서 카운트 감소
        
        // 만약 음수로 감소할 경우 0으로 강제 설정
        if (_activeBulletCount < 0) _activeBulletCount = 0;
    }
}
