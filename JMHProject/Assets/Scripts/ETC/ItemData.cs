using System.Collections.Generic;
using UnityEngine;

// 아이템 타입
public enum ItemType { Short, Long }

// 레벨별 상세 수치를 담는 구조체
[System.Serializable]
public struct ItemLevelData
{
    public int damage;      // 해당 레벨의 데미지
    public int amount;      // 해당 레벨의 무기 개수 (targetCount)
    public float cooltime;  // 해당 레벨의 쿨타임
    public float speed;     // 해당 레벨의 속도
    public int pierce;      // 관통 횟수
    [TextArea]
    public string desc;     // 레벨업 시 화면에 보여줄 설명 (예: "도끼 개수 +1")
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;

    [Header("Prefabs")]
    public GameObject prefab;       
    public GameObject bulletPrefab; 

    [Header("Level Data List")]
    // 리스트의 Index 0이 1레벨, Index 1이 2레벨 정보를 담는 형식
    public List<ItemLevelData> levelDataList = new List<ItemLevelData>();

    // 현재 레벨을 넣으면 해당 데이터를 반환하는 함수
    public ItemLevelData GetLevelData(int level)
    {
        // 레벨은 1부터 시작하지만 리스트는 0부터 시작하므로 -1을 뺌
        int index = Mathf.Clamp(level - 1, 0, levelDataList.Count - 1);
        return levelDataList[index];
    }
}

