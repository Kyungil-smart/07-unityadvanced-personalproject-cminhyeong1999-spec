using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private int spawnCount = 5;
    
    private readonly int _spawnMax = 300;
    
    void Start()
    {
        InvokeRepeating(nameof(Spawn), 2f, spawnRate);
        EventManager.Instance.LevelChanged += MonsterLevelChange;
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject monster = MonsterPoolManager.Instance.GetObject();
            monster.transform.position = GetRandomSpawnPosition();
        }
    }

    private void MonsterLevelChange(StageLevel level)
    {
        //PoolManager.Instance.SetSummonTypes();
    }

    // 플레이어 위치를 기준으로 시야 밖에 원형 구역에 몹을 스폰하기 위한 위치 설정 메서드
    private Vector2 GetRandomSpawnPosition()
    {
        // 위치를 참조할 플레이어가 없으면 제로벡터 리턴
        if (PlayerPresenter.Player == null) return Vector2.zero;
        
        // 플레이어 위치
        Vector2 playerPos = PlayerPresenter.Player.GetPos();

        // 스폰 범위 설정 / 현재 카메라 시야가 Size 4 임을 감안하여 설정
        float minDistance = 7f;  // 화면에 안 보이는 최소 거리
        float maxDistance = 12f; // 적당한 최대 거리

        // 랜덤한 방향(단위 원) 세팅
        Vector2 randomDir = Random.insideUnitCircle.normalized;

        // 최소와 최대 사이의 랜덤한 거리를 원에 곱함
        float randomDist = Random.Range(minDistance, maxDistance);

        // 최종 좌표 계산
        Vector2 spawnPos = playerPos + new Vector2(randomDir.x, randomDir.y) * randomDist;

        return spawnPos;
    }

    private void OnDisable()
    {
        EventManager.Instance.LevelChanged -= MonsterLevelChange;
    }

}
