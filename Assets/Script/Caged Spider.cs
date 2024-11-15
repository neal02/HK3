using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CagedSpider : MonoBehaviour
{
    private Animator animator;
    public string animationTriggerName = "Spider death";  // 애니메이션 트리거 이름
    public Tilemap currentTilemap;  // 현재 타일맵
    public Tilemap nextTilemap;     // 다음 타일맵

    private void Start()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    // 트리거 충돌 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트의 태그가 특정 태그인지 확인 후 애니메이션 재생
        if (other.CompareTag("Attack"))  // 필요한 태그로 교체 가능
        {
            animator.SetTrigger(animationTriggerName);  // 애니메이션 파라미터 설정

            // 애니메이션 길이 확인 후 파괴
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float animationLength = stateInfo.length;
            Destroy(gameObject, animationLength);// 애니메이션이 끝난 후 오브젝트 파괴
            OnDestroy();
        }
    }

    private void OnDestroy()
    {
        // 오브젝트가 파괴될 때 타일맵 변경
        if (currentTilemap != null) {
            currentTilemap.gameObject.SetActive(false);  // 현재 타일맵 비활성화
        }

        if (nextTilemap != null) {
            nextTilemap.gameObject.SetActive(true);      // 다음 타일맵 활성화
        }
    }
}
