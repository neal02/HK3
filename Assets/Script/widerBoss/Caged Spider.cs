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

    private void Start()
    {
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();
    }

    // Ʈ���� �浹 ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� �±װ� Ư�� �±����� Ȯ�� �� �ִϸ��̼� ���
        if (other.CompareTag("Attack"))  // �ʿ��� �±׷� ��ü ����
        {
            animator.SetTrigger(animationTriggerName);  // �ִϸ��̼� �Ķ���� ����

            // �ִϸ��̼� ���� Ȯ�� �� �ı�
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float animationLength = stateInfo.length;
            Destroy(gameObject, 0.6f);// �ִϸ��̼��� ���� �� ������Ʈ �ı�
        }
    }

    private void OnDestroy()
    {
        // ������Ʈ�� �ı��� �� Ÿ�ϸ� ����
        if (currentTilemap != null) {
            currentTilemap.gameObject.SetActive(false);  // ���� Ÿ�ϸ� ��Ȱ��ȭ
        }

        if (DestroyTilemap != null) {
            DestroyTilemap.gameObject.SetActive(false);  // ���� Ÿ�ϸ� ��Ȱ��ȭ
        }
        if (nextTilemap != null) {
            nextTilemap.gameObject.SetActive(true);      // ���� Ÿ�ϸ� Ȱ��ȭ
        }
    }
}
