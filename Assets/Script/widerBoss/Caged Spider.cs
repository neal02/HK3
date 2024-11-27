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
        // ������Ʈ�� �ı��� �� �Ҹ� ���
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.destroy);  // 'destroy' ȿ���� ���
        }
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
        if (nextTilemap2 != null) {
            nextTilemap2.gameObject.SetActive(true);      // ���� Ÿ�ϸ� Ȱ��ȭ
        }
    }
}
