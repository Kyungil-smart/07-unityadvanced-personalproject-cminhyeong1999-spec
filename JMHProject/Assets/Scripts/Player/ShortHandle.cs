using System.Collections.Generic;
using UnityEngine;

public class ShortHandle : MonoBehaviour
{
    public ItemData weaponData;
    public int currentLevel = 1;

    // 이 핸들이 관리하는 실제 무기들 (예: 도끼 1, 도끼 2...)
    private List<Transform> _instances = new();
    
    // 현재 레벨의 데이터 프로퍼티
    public int Amount => weaponData.GetLevelData(currentLevel).amount;
    //public float Speed => weaponData.GetLevelData(currentLevel).speed;
    public float Speed 
    {
        get 
        {
            // 데이터가 아직 할당 안 됐으면 0 반환
            if (weaponData == null) return 0; 
            return weaponData.GetLevelData(currentLevel).speed;
        }
    }

    public void Init(ItemData data)
    {
        weaponData = data;
        currentLevel = 1;
    }

    public void LevelUp()
    {
        // 최대 레벨 체크 후 증가
        if (currentLevel < weaponData.levelDataList.Count)
        {
            currentLevel++;
            RebatchAll(); // 수치 변경에 따른 재배치
        }
    }

    void Update()
    {
        // 부모(이 오브젝트)가 회전하면 자식들은 자동으로 공전함
        transform.Rotate( Speed * Time.deltaTime * Vector3.back);
    }

    public void AddWeaponInstance(Transform weaponTransform)
    {
        _instances.Add(weaponTransform);
        RebatchAll();
    }
    
    public void RebatchAll()
    {
        // 재배치 시 기존 위치들을 초기화
        _instances.Clear();
        foreach (Transform child in transform)
        {
            _instances.Add(child);
        }
        
        int count = _instances.Count;
        if (count == 0) return;   

        for (int i = 0; i < count; i++)
        {
            Transform weapon = _instances[i];
            weapon.localPosition = Vector3.zero;

            // 원형 배치 계산
            float angle = (360f / count) * i;
            weapon.localRotation = Quaternion.Euler(0, 0, angle);
            
            // '위쪽' 방향 벡터를 각도만큼 회전시킨 후 거리(1.5f)를 곱함
            Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector2.up;
            weapon.localPosition = dir * 1.5f;
        }
    }
    
}
