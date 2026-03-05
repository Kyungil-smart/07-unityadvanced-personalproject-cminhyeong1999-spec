using UnityEngine;

public class HomingBullet : Bullet
{
    private Transform _target;
    [SerializeField] private float _detectRange = 10f; // 총알 기준 적 감지 범위
    [SerializeField] private float _rotateSpeed = 5f;  // 추적시 회전 강도 

    void Update()
    {
        // 타겟이 없거나 비활성화되었다면 새로 찾기
        if (_target == null || !_target.gameObject.activeSelf)
        {
            _target = FindNearestEnemy();
        }

        // 타겟이 있다면 방향 수정
        if (_target != null)
        {
            Vector3 targetDir = (_target.position - transform.position).normalized;
            // 현재 방향에서 타겟 방향으로 서서히 접근
            _direction = Vector3.Lerp(_direction, targetDir, Time.deltaTime * _rotateSpeed).normalized;
        }

        // 이동 
        transform.position += _speed * Time.deltaTime * _direction;
    }

    private Transform FindNearestEnemy()
    {
        // 총알 주변 _detectRange 반경 안에 있는 'Enemy' 레이어의 충돌체를 전부 가져옴
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _detectRange, LayerMask.GetMask("Enemy"));

        // 첫 타겟은 발견 못한 상태
        Transform nearest = null;
        // 제일 가까운 거리의 초기 설정을 최대치로 설정
        float minDist = _detectRange;

        // 가져온 충돌체들을 순회 돔
        foreach (var collider in colliders)
        {
            // 충돌체와 거리 계산
            float dist = Vector2.Distance(transform.position, collider.transform.position);
            
            // 검사한 충돌체와의 거리가 minDist보다 작으면
            // minDist를 이 거리로 갱신하고
            // 타겟을 그 충돌체로 선택
            if (dist < minDist)
            {
                minDist = dist;
                nearest = collider.transform;
            }
        }
        return nearest;
    }
}