using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public Transform targetPortal; // 이동할 포탈의 위치를 설정
    public Animator portalAnimator; // 포탈 애니메이터
    public float animationDelay = 0.01f; // 애니메이션 지연 시간 (초)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 충돌한 오브젝트가 Player인지 확인
        {
            if (targetPortal != null) // 타겟 포탈이 설정되어 있는지 확인
            {
                StartCoroutine(TeleportPlayer(collision)); // 코루틴으로 이동 처리
            }
            else
            {
                Debug.LogWarning("Target portal is not assigned!"); // 디버그 메시지
            }
        }
    }

    private IEnumerator TeleportPlayer(Collider2D player)
    {
        // 1. 포탈 애니메이션 상태를 'Warp'로 전환
        if (portalAnimator != null)
        {
            portalAnimator.SetTrigger("Warp");
        }

        // 2. 애니메이션 딜레이만큼 대기
        yield return new WaitForSeconds(animationDelay);

        // 3. 플레이어 위치를 타겟 포탈 위치로 이동 (약간 위로 이동)
        player.transform.position = targetPortal.position + new Vector3(0, 0.5f, 0);

        // 4. 포탈 애니메이션 상태를 다시 'Idle'로 복구
        if (portalAnimator != null)
        {
            portalAnimator.SetTrigger("Idle");
        }
    }
}
