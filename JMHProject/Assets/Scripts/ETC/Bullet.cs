using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject _originPrefab;   // 오브젝트 풀에 수납,반납할 경우 참고하기 위한 프리펩
    private Coroutine _lifeTimeCoroutine; // 코루틴 참조 저장용
    
    public int Damage;
    public int Pierce; // 관통 횟수
    
    protected Vector3 _direction;
    protected float _speed;

    public void Init(GameObject prefab,int damage, int pierce, Vector3 direction, float speed)
    {
        _originPrefab = prefab;
        Damage = damage;
        Pierce = pierce;
        _direction = direction;
        _speed = speed;
        
        // 기존에 돌고 있던 코루틴이 있다면 정지
        if (_lifeTimeCoroutine != null) StopCoroutine(_lifeTimeCoroutine);
        
        // 새로운 수명 코루틴 시작 (예: 5초 뒤 자동 반납)
        _lifeTimeCoroutine = StartCoroutine(LifeTimeRoutine(5.0f));
    }
    
    private IEnumerator LifeTimeRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToPool();
    }
    
    // 일정 시간 이후까지 총알이 남아있으면 강제로 반납
    public void ReturnToPool()
    {
        if (!gameObject.activeSelf) return; // 중복 반납 방지
        
        // 코루틴 정지
        if (_lifeTimeCoroutine != null)
        {
            StopCoroutine(_lifeTimeCoroutine);
            _lifeTimeCoroutine = null;
        }
        
        BulletPoolManager.Instance.Release(_originPrefab, gameObject);
    }

    private void Update()
    {
        transform.position += _speed * Time.deltaTime * _direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Hit(Damage);
            Pierce--;
            if (Pierce < 0) BulletPoolManager.Instance.Release(_originPrefab, gameObject);
        }
    }
    
    // 오브젝트가 비활성화될 때 코루틴이 멈춰있지 않았을 경우 멈춤
    private void OnDisable()
    {
        if (_lifeTimeCoroutine != null)
        {
            StopCoroutine(_lifeTimeCoroutine);
            _lifeTimeCoroutine = null;
        }
    }
}