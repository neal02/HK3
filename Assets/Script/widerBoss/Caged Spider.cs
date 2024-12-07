using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CagedSpider : MonoBehaviour
{
    private Animator animator;
    public string animationTriggerName = "Hit";  // �ִϸ��̼� Ʈ���� �̸�
    public Tilemap currentTilemap;  // ���� Ÿ�ϸ�
    public Tilemap DestroyTilemap;  // ���� Ÿ�ϸ�
    public Tilemap nextTilemap;     // ���� Ÿ�ϸ�
    public Tilemap nextTilemap2;     // ���� Ÿ�ϸ�

    private void Start()
    {
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();
    }

    // Ʈ���� �浹 ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack")) {
            animator.SetTrigger(animationTriggerName);

            // �ִϸ��̼� ���� Ȯ�� �� �ڷ�ƾ���� ó��
            StartCoroutine(DestroyAfterDelay(0.6f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Ÿ�ϸ� ���� �۾�
        if (currentTilemap != null)
            currentTilemap.gameObject.SetActive(false);

        if (DestroyTilemap != null)
            DestroyTilemap.gameObject.SetActive(false);

        if (nextTilemap != null)
            nextTilemap.gameObject.SetActive(true);

        if (nextTilemap2 != null)
            nextTilemap2.gameObject.SetActive(true);

        // ���� �ð� �� ������Ʈ �ı�
        Destroy(gameObject);
    }
}