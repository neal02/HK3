using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CagedSpider : MonoBehaviour
{
    private Animator animator;
    public string animationTriggerName = "Hit";  // 애니메이션 트리거 이름
    public Tilemap currentTilemap;  // 현재 타일맵
    public Tilemap DestroyTilemap;  // 현재 타일맵
    public Tilemap nextTilemap;     // 다음 타일맵
    public Tilemap nextTilemap2;     // 다음 타일맵

    private void Start()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    // 트리거 충돌 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack")) {
            animator.SetTrigger(animationTriggerName);

            // 애니메이션 길이 확인 후 코루틴으로 처리
            StartCoroutine(DestroyAfterDelay(0.6f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // 타일맵 변경 작업
        if (currentTilemap != null)
            currentTilemap.gameObject.SetActive(false);

        if (DestroyTilemap != null)
            DestroyTilemap.gameObject.SetActive(false);

        if (nextTilemap != null)
            nextTilemap.gameObject.SetActive(true);

        if (nextTilemap2 != null)
            nextTilemap2.gameObject.SetActive(true);

        // 일정 시간 후 오브젝트 파괴
        Destroy(gameObject);
    }
}