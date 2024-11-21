using UnityEngine;

public class AxeDamage : MonoBehaviour
{
    public int damage = 100; // 플레이어에게 줄 데미지

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 충돌한 오브젝트가 Player인지 확인
        {
            playerControl player = collision.GetComponent<playerControl>();
            if (player != null)
            {
                player.TakeDamage(damage); // 플레이어에게 데미지 적용
                Debug.Log("Player took damage from Axe: " + damage);
            }
            else
            {
                Debug.LogWarning("PlayerControl 스크립트가 Player 오브젝트에 없습니다.");
            }
        }
    }
}
