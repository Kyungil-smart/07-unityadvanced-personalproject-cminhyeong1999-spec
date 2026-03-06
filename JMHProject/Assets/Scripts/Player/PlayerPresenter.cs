using UnityEngine;

public class PlayerPresenter //: MonoBehaviour
{
    private PlayerModel _playermodel;
    private PlayerView _playerview;
    public static PlayerPresenter Player;

    public Vector2 lastDir; // 가만히 있을 경우 총알이 발사 될 방향
    
    public PlayerPresenter(PlayerModel model, PlayerView view)
    {
        _playermodel = model;
        _playerview = view;
        Player = this;
        
        lastDir = Vector2.right;    // 처음 총을 오른손에 쥐고 있는 이미지라 오른쪽으로 설정

        InputManager.Instance.Walk += Walk;
        EventManager.Instance.OnHpDecreased += TakeDamage;
        EventManager.Instance.OnHpIncreased += HealHp;
        EventManager.Instance.OnExpIncreased += GainExp;
    }

    private void Walk(Vector2 input)
    {
        float speed = _playermodel._moveSpeed;
        Vector2 currentVel = _playermodel.CurrentVelocity;

        currentVel = input * speed;
        _playermodel.CurrentVelocity = currentVel;
        _playerview.SetVelocity(currentVel);
        _playerview.ani.SetFloat("Speed", Mathf.Abs(input.magnitude));  // 플레이어 애니매이션 전환용, 움직이면 1, 가만히 있으면 0
        _playerview.SetFlipped(currentVel);  // 이미지 좌우 반전
        SetPlayerPos(); // 플레이어의 절대 좌표 저장
        
        // 가만히 있을 경우 총알이 발사 될 방향 결졍하기 위함
        if (input.sqrMagnitude > 0)
        {
            lastDir = input.normalized;
        }
    }

    public Vector2 GetPos()
    {
        return _playermodel.Pos;
    }

    public Transform GetVisual()
    {
        return _playerview.visualTransform;
    }

    public void SetPlayerPos()
    {
        _playermodel.Pos = _playerview.GetPos();
    }

    public int GetDamage(int baseDamage)
    {
        return _playermodel.GetDamage(baseDamage);
    }

    public void TakeDamage(int damage)
    {
        _playermodel.TakeDamage(damage);
    }

    public void HealHp(int heal)
    {
        _playermodel.HealHp(heal);
    }

    public int GetMaxHp()
    {
        return _playermodel._maxHp;
    }

    public int GetMaxExp()
    {
        return _playermodel._maxExp;
    }

    public int GetCurrentLevel()
    {
        return _playermodel.GetLevel();
    }
    
    // 경험치를 먹고 레벨업이 가능하면 레벨업 이벤트 발행
    public void GainExp(int exp)
    {
        bool what = _playermodel.IsLevelUp(exp);
        if (what) PlayerLevelUp();
    }

    // 레벨업시 이벤트 발행 및 무기 선택
    public void PlayerLevelUp()
    {
        EventManager.Instance.PublishOnPlayerLevelUp();
        Object.FindFirstObjectByType<SelectWeaponManager>().OpenUI();
    }
    
    public void Terminate()
    {
        InputManager.Instance.Walk -= Walk;
        EventManager.Instance.OnHpDecreased -= TakeDamage;
        EventManager.Instance.OnHpIncreased -= HealHp;
        _playerview.gameObject.SetActive(false);
        _playermodel = null;
    }
}
