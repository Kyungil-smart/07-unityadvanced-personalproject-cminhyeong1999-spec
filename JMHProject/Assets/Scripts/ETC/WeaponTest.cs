using UnityEngine;

public class WeaponTest : MonoBehaviour
{
    public WeaponHandle weaponHandle;
    public ItemData axeData;
    public ItemData knifeData;

    private void Start()
    {
        // InputManager의 Click 이벤트 구독
        if (InputManager.Instance != null)
        {
            InputManager.Instance.Click += OnInputClick;
        }
    }

    private void OnInputClick(string controlName)
    {
        // controlName은 누른 버튼의 이름입니다 (예: "leftButton", "1", "2" 등)
        // 사용하시는 NewInputSystem 설정에 따라 이름이 다를 수 있으니 로그로 확인해보세요.

        if (controlName == "leftButton") // 숫자 키 1을 눌렀을 때
        {
            weaponHandle.AddWeapon(axeData);
        }

    }

    private void OnDestroy()
    {
        // 메모리 누수 방지를 위해 이벤트 구독 해제
        if (InputManager.Instance != null)
        {
            InputManager.Instance.Click -= OnInputClick;
        }
    }
}