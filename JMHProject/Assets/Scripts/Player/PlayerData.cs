using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("기본 스텟")]
    public string baseName = "PLAYER";
    public int maxHp = 100;
    public int heal = 0;    // 자연 회복 되는 수치
    public int startExp = 5;            // 1 -> 2 레벨업을 위한 최대 경험치
    public int startEnforcePower = 0;    // 초기 추가 데미지 수치
    public float baseMoveSpeed = 7f;
    public float maxSpeedLimit = 70f;
    
    
    [Header("성장 계수")]
    public int enforcePower = 1;    // 레벨당 추가 데미지 수치
    public int hpGainPerLevel = 1;    // 레벨당 HP 증가량
    public int expStep1 = 7; // 20레벨 전까지의 증가량
    public int expStep2 = 10; // 20레벨 전까지의 증가량
    public int expStep3 = 13; // 20레벨 전까지의 증가량
    public float addSpeedLevel = 0.5f; // 레벨당 속도 증가량
    
    [Header("경험치 벽")]   // 뱀서에서 특정 레벨에 의도적으로 레벨업을 더디게 하기 위한 장치
    public int wall20 = 400;         // 20레벨 구간 보너스 필요량
    public int wall40 = 1600;        // 40레벨 구간 보너스 필요량
    
    // 레벨을 넣으면 공식을 계산해서 해당 레벨의 최대 경험치를 밷는 함수
    public int GetMaxExp(int level)
    {
        if (level <= 1) return startExp;

        // 뱀서식 공식 (if문으로 구간을 나눔)
        if (level < 20)
            return (level * expStep1) - 5;
        
        if (level == 20)
            return (level * expStep1) - 5 + wall20;

        if (level < 40)
            return (level * expStep2) - 6;

        if (level == 40)
            return (level * expStep2) - 6 + wall40;

        // 41레벨 이상
        return (level * expStep3) - 8;
    }
}
