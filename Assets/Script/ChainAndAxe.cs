using UnityEngine;

public class ChainAndAxe : MonoBehaviour
{
    public float speed = 2f; // �̵� �ӵ�
    public float distance = 3f; // ���Ʒ� �̵� ����
    public int damage = 100; // �÷��̾�� �� ������

    private Vector3 startPosition; // ���� ��ġ ����

    void Start()
    {
        startPosition = transform.position; // �ʱ� ��ġ ����
    }

    void Update()
    {
        // ���Ʒ��� �����̰� �ϴ� ���� (Sin �Լ� ���)
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * distance;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // �浹�� ������Ʈ�� Player���� Ȯ��
        {
            playerControl player = collision.GetComponent<playerControl>();
            if (player != null)
            {
                player.TakeDamage(damage); // �÷��̾�� ������ ����
                Debug.Log("Player took damage from Chain or Axe: " + damage);
            }
            else
            {
                Debug.LogWarning("PlayerControl ��ũ��Ʈ�� Player ������Ʈ�� �����ϴ�.");
            }
        }
    }
}
