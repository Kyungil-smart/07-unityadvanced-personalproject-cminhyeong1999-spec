using UnityEngine;

public class WeaponHandle : MonoBehaviour
{
    public Transform shortWeapon; 
    public Transform longGroup; 

    public void AddWeapon(ItemData data)
    {
        // 해당 무기 이름을 가진 핸들이 이미 있는지 확인
        Transform targetGroup = (data.itemType == ItemType.Short) ? shortWeapon : longGroup;
        Transform existingHandle = targetGroup.Find(data.itemName); // ItemData의 itemName 사용
        
        // 새로 만들거나 기존 것을 가져올 때 추출된 컴포넌트를 담아두었다가
        // 프리펩 생성과정에서 사용하기 위함
        ShortHandle sHandle = null;

        // 핸들이 없으면 새로 생성
        if (existingHandle == null)
        {
            GameObject handleGo = new GameObject(data.itemName);
            handleGo.transform.SetParent(targetGroup, false);
            
            if (data.itemType == ItemType.Short) 
            {
                // 생성 후 근접무기 컴포넌트 부착
                sHandle = handleGo.AddComponent<ShortHandle>();
                sHandle.Init(data); // 초기 데이터 설정
            }
            else 
            {
                // 원거리는 계획대로 추후 구현
                // 생성 후 원거리무기 컴포넌트 부착
                var lHandle = handleGo.AddComponent<LongHandle>();
                lHandle.weaponData = data;
                return;
            }
        }
        else
        {
            // 이미 있다면 레벨업 처리
            if (data.itemType == ItemType.Short)
            {
                sHandle = existingHandle.GetComponent<ShortHandle>();
                sHandle.LevelUp(); 
            }
        }

        // 근거리 무기인 경우, 레벨에 맞는 개수만큼 프리펩 생성 및 동기화
        if (data.itemType == ItemType.Short && sHandle != null)
        {
            // 현재 자식 개수와 데이터상의 목표 개수(Amount) 비교
            int currentCount = sHandle.transform.childCount;
            int targetCount = sHandle.Amount;

            for (int i = currentCount; i < targetCount; i++)
            {
                GameObject sWeapon = Instantiate(data.prefab, sHandle.transform);
                sHandle.AddWeaponInstance(sWeapon.transform);
            }
        }
    }
}
