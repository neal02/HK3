using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public Transform targetPortal; // �̵��� ��Ż�� ��ġ�� ����
    public Animator portalAnimator; // ��Ż �ִϸ�����
    public float animationDelay = 0.01f; // �ִϸ��̼� ���� �ð� (��)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // �浹�� ������Ʈ�� Player���� Ȯ��
        {
            if (targetPortal != null) // Ÿ�� ��Ż�� �����Ǿ� �ִ��� Ȯ��
            {
                StartCoroutine(TeleportPlayer(collision)); // �ڷ�ƾ���� �̵� ó��
            }
            else
            {
                Debug.LogWarning("Target portal is not assigned!"); // ����� �޽���
            }
        }
    }

    private IEnumerator TeleportPlayer(Collider2D player)
    {
        // 1. ��Ż �ִϸ��̼� ���¸� 'Warp'�� ��ȯ
        if (portalAnimator != null)
        {
            portalAnimator.SetTrigger("Warp");
        }

        // 2. �ִϸ��̼� �����̸�ŭ ���
        yield return new WaitForSeconds(animationDelay);

        // 3. �÷��̾� ��ġ�� Ÿ�� ��Ż ��ġ�� �̵� (�ణ ���� �̵�)
        player.transform.position = targetPortal.position + new Vector3(0, 0.5f, 0);

        // 4. ��Ż �ִϸ��̼� ���¸� �ٽ� 'Idle'�� ����
        if (portalAnimator != null)
        {
            portalAnimator.SetTrigger("Idle");
        }
    }
}
