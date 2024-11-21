using UnityEngine;

public class ChainAndAxe : MonoBehaviour
{
    public float speed = 2f; // 이동 속도
    public float distance = 3f; // 위아래 이동 범위
    public int damage = 100; // 플레이어에게 줄 데미지

    private Vector3 startPosition; // 시작 위치 저장

    void Start()
    {
        startPosition = transform.position; // 초기 위치 저장
    }

    void Update()
    {
        // 위아래로 움직이게 하는 로직 (Sin 함수 사용)
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * distance;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 충돌한 오브젝트가 Player인지 확인
        {
            playerControl player = collision.GetComponent<playerControl>();
            if (player != null)
            {
                player.TakeDamage(damage); // 플레이어에게 데미지 적용
                Debug.Log("Player took damage from Chain or Axe: " + damage);
            }
            else
            {
                Debug.LogWarning("PlayerControl 스크립트가 Player 오브젝트에 없습니다.");
            }
        }
    }
}
