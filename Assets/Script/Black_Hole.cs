using System.Collections;
using UnityEngine;

public class Black_Hole : MonoBehaviour
{
    public float pullForce = 500f;  // ������� ��
    public float pullRadius = 5f;   // ȿ�� ���� (�÷��̾ ������ ����)
    public LayerMask playerLayer;  // �÷��̾� ���̾�
    public int damage = 10;        // �÷��̾�� �� ������

    private bool isActive = true;  // ������� Ȱ��ȭ ����

    private CircleCollider2D col;  // CircleCollider2D ������Ʈ

    void Start()
    {
        // Rigidbody2D �߰� �� �߷� Ȱ��ȭ
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 1; // ��Ȧ�� ���������� ����

        // CircleCollider2D ������Ʈ ��������
        col = GetComponent<CircleCollider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<CircleCollider2D>();
        }
        col.isTrigger = true;  // �浹�� Ʈ���ŷ� ó��, �������� Ʈ���� ������θ� �־���
        col.radius = 0.04f;    // ��Ȧ�� ������ ������ ����

        // ��Ȧ�� �������� �������� �÷��̾ ������� ����
        StartCoroutine(PullObjects());
    }

    // Ʈ���� ���� ���� �÷��̾�Ը� ������ ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾�� �浹 �� ü�� ����
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
            // ������ ���� ���� �÷��̾� Ž�� (pullRadius�� ��������)
            Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, pullRadius, playerLayer);

            foreach (var player in players)
            {
                // �÷��̾ ���� ���� �������
                if (player != null)
                {
                    Vector2 direction = (transform.position - player.transform.position).normalized;
                    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                    if (playerRb != null)
                    {
                        // ������� �� ����
                        playerRb.AddForce(direction * pullForce * Time.deltaTime, ForceMode2D.Force);
                    }
                    else
                    {
                        Debug.LogWarning("Player does not have a Rigidbody2D component!");
                    }
                }
            }

            yield return null; // �� �����Ӹ��� �ݺ�
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ������� ���� �ð�ȭ (pullRadius)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}
