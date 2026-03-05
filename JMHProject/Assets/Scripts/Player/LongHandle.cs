using System.Collections;
using UnityEngine;

public class LongHandle : MonoBehaviour
{
    public ItemData weaponData;
    public int currentLevel = 1;
    private float _timer;
 
    // 현재 레벨 데이터 가져오기
    public ItemLevelData CurrentData => weaponData.GetLevelData(currentLevel);

    public void Init(ItemData data)
    {
        weaponData = data;
        currentLevel = 1;
    }

    void Update()
    {
        if (weaponData == null) return;
        
        _timer += Time.deltaTime;

        if (_timer >= CurrentData.cooltime)
        {
            _timer = 0f;
            StartCoroutine(FireRoutine());
        }
    }
    
    IEnumerator FireRoutine()
    {
        // 총 발사할 투사체
        int count = CurrentData.amount; 
        float interval = 0.1f; // 화살/총알 사이의 아주 짧은 간격

        for (int i = 0; i < count; i++)
        {
            Fire(); // 발사할 만큼 투사체 발사
        
            // 잠깐 대기
            yield return new WaitForSeconds(interval);
        }
    }

    void Fire()
    {
        // 플레이어가 기억하는 마지막 방향
        Vector2 currentVel = PlayerPresenter.Player.lastDir;
        // 발사될 위치
        Vector3 firePos = PlayerPresenter.Player.GetVisual().position;

        // 총알 생성
        GameObject bulletShot = BulletPoolManager.Instance.Get(weaponData.bulletPrefab, firePos, Quaternion.identity);
        
        if (bulletShot == null) return;
        
        if (bulletShot.TryGetComponent(out Bullet bullet))
        {
            // 원본 프리펩과 데이터를 넘겨줌
            bullet.Init(weaponData.bulletPrefab, CurrentData.damage, CurrentData.pierce, currentVel, CurrentData.speed);
        }
    }

    public void LevelUp()
    {
        if (currentLevel < weaponData.levelDataList.Count)
            currentLevel++;
    }
}