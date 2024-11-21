using UnityEngine;

public class AxeDamage : MonoBehaviour
{
    public int damage = 100; // �÷��̾�� �� ������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // �浹�� ������Ʈ�� Player���� Ȯ��
        {
            playerControl player = collision.GetComponent<playerControl>();
            if (player != null)
            {
                player.TakeDamage(damage); // �÷��̾�� ������ ����
                Debug.Log("Player took damage from Axe: " + damage);
            }
            else
            {
                Debug.LogWarning("PlayerControl ��ũ��Ʈ�� Player ������Ʈ�� �����ϴ�.");
            }
        }
    }
}
