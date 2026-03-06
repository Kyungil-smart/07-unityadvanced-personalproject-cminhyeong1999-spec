using UnityEngine;
using UnityEngine.UI;

public class SelectWeaponManager : MonoBehaviour
{
    public GameObject selectionPanel; // 무기 선택 UI 패널
    public WeaponHandle weaponHandle; // 플레이어에게 붙어있는 WeaponHandle
    
    // 버튼 3개를 인스펙터에서 직접 할당 (미사일, 총, 도끼)
    public Button[] weaponButtons;
    public ItemData[] fixedItemDatas; // 각 버튼에 매칭될 데이터 3개

    void OnEnable()
    {
        selectionPanel.SetActive(false);

        // 버튼 클릭 이벤트 연결 (인덱스 0, 1, 2)
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            int index = i; 
            weaponButtons[i].onClick.AddListener(() => OnSelected(index));
        }
    }

    void Start()
    {
        OpenUI();
    }
    
    // 버튼 열릴때 멈출 예정
    public void OpenUI()
    {
        selectionPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnSelected(int index)
    {
        // 데이터 전달
        weaponHandle.AddWeapon(fixedItemDatas[index]);

        // UI 비활성화 및 재개
        selectionPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
