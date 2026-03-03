using UnityEngine;

public class relocation : MonoBehaviour
{
    [SerializeField] private Transform[] tiles; // 자식 타일
    [SerializeField] private float mapSize = 40f; // 2x2 전체 크기 (20+20=40)
    private readonly float _checkDistance = 20f;

    void LateUpdate()
    {
        Vector3 playerPos = PlayerPresenter.Player.GetPos();
        
        
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
