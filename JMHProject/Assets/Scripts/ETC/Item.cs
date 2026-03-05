using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int currentLevel = 1;

    // 외부에서 물어볼 때 호출하는 프로퍼티
    public int Damage => data.levelDataList[currentLevel - 1].damage;
    public int Amount => data.levelDataList[currentLevel - 1].amount;

    // 초기화 함수 (생성 시 호출)
    public void Init(ItemData newData, int level)
    {
        data = newData;
        currentLevel = level;
    }
}
