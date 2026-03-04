using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class relocation : MonoBehaviour
{
    [SerializeField] private Transform[] tiles; // 자식 타일
    [SerializeField] private float mapSize = 40f; // 2x2 전체 크기 (20+20=40)
    private readonly float _checkDistance = 20f;
    private Vector3 _playerPos;
    
    private void LateUpdate()
    {   
        // 키가 눌렸다 뗏을때만 PlayerPos가 업데이트 되는 구조 때문에
        // 키가 눌려지는동안 타일맵들이 이동을 하지 않는 현상을 개선하기 위하여
        // LateUpdate에서 PlayerPos를 설정
        PlayerPresenter.Player.SetPlayerPos();
        _playerPos = PlayerPresenter.Player.GetPos();
        MoveTiles(_playerPos);
    }

    private void MoveTiles(Vector3 playerPos)
    {
        foreach (Transform tile in tiles)
        {
            // 플레이어와 각 타일 사이의 거리 차이 계산
            float diffX = playerPos.x - tile.position.x;
            float diffY = playerPos.y - tile.position.y;

            // X축 재배치: 거리 차이가 checkDistance보다 커지면 모든 타일들을 40칸 점프
            if (Mathf.Abs(diffX) > _checkDistance)
            {
                float dirX = diffX > 0 ? 1 : -1;
                tile.position += dirX * mapSize * Vector3.right;
            }

            // Y축 재배치
            if (Mathf.Abs(diffY) > _checkDistance)
            {
                float dirY = diffY > 0 ? 1 : -1;
                tile.position += dirY * mapSize * Vector3.up;
            }
        }
    }
    
}
