using System.Collections;
using UnityEngine;

public class Black_Hole : MonoBehaviour
{
    public float pullForce = 500f;  // 끌어당기는 힘
    public float pullRadius = 5f;   // 효과 범위 (플레이어를 끌어당길 범위)
    public LayerMask playerLayer;  // 플레이어 레이어
    public int damage = 10;        // 플레이어에게 줄 데미지

    private bool isActive = true;  // 끌어당기기 활성화 상태

    private CircleCollider2D col;  // CircleCollider2D 컴포넌트

    void Start()
    {
        // Rigidbody2D 추가 및 중력 활성화
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 1; // 블랙홀이 떨어지도록 설정

        // CircleCollider2D 컴포넌트 가져오기
        col = GetComponent<CircleCollider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<CircleCollider2D>();
        }
        col.isTrigger = true;  // 충돌은 트리거로 처리, 데미지는 트리거 방식으로만 주어짐
        col.radius = 0.04f;    // 블랙홀의 물리적 반지름 설정

        // 블랙홀이 떨어지기 전에부터 플레이어를 끌어당기기 시작
        StartCoroutine(PullObjects());
    }

    // 트리거 범위 내의 플레이어에게만 데미지 적용
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어와 충돌 시 체력 감소
            playerControl player = other.GetComponent<playerControl>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log($"Player took {damage} damage from Black Hole!");
            }
        }
    }

    private IEnumerator PullObjects()
    {
        while (isActive)
        {
            // 지정된 범위 안의 플레이어 탐색 (pullRadius를 기준으로)
            Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, pullRadius, playerLayer);

            foreach (var player in players)
            {
                // 플레이어가 있을 때만 끌어당기기
                if (player != null)
                {
                    Vector2 direction = (transform.position - player.transform.position).normalized;
                    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                    if (playerRb != null)
                    {
                        // 끌어당기는 힘 적용
                        playerRb.AddForce(direction * pullForce * Time.deltaTime, ForceMode2D.Force);
                    }
                    else
                    {
                        Debug.LogWarning("Player does not have a Rigidbody2D component!");
                    }
                }
            }

            yield return null; // 매 프레임마다 반복
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 끌어당기는 범위 시각화 (pullRadius)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}
