using UnityEngine;

public class WeaponTest : MonoBehaviour
{
    public WeaponHandle weaponHandle;
    public ItemData axeData;
    public ItemData pistolData;
    public ItemData HotdogData;

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

        if (controlName == "leftButton") 
        {
            weaponHandle.AddWeapon(axeData);
        }

        if (controlName == "rightButton")
        {
            bool what = (Random.value < 0.5f);
            
            if (what)
            {
                weaponHandle.AddWeapon(pistolData);
            }
            else
            {
                weaponHandle.AddWeapon(HotdogData);
            }
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