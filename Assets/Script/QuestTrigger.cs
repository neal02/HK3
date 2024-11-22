using UnityEngine;
using UnityEngine.UI;  // UI ����� ����ϱ� ���� �ʿ�

public class QuestTrigger : MonoBehaviour
{
    public GameObject player;  // �÷��̾� ��ü
    public Image enterImage;   // UI �̹����� ǥ�õ� "Enter" �̹���

    private void Start()
    {
        // ó������ "Enter" �̹����� ��Ȱ��ȭ
        enterImage.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ Quest ������Ʈ�� ������
        if (other.CompareTag("Player")) {
            enterImage.gameObject.SetActive(true);  // �̹��� Ȱ��ȭ

            // �÷��̾��� ���� ��ǥ�� ȭ�� ��ǥ�� ��ȯ
            Vector3 playerPosition = player.transform.position;
            playerPosition.y += 1.8f; // �Ӹ� �� ��ġ (y=1f�� ���Ͽ� �Ӹ� ���� �̵�)

            // ���� ��ǥ�� ȭ�� ��ǥ�� ��ȯ
            Vector3 screenPos = Camera.main.WorldToScreenPoint(playerPosition);

            // �̹��� ��ġ�� ȭ�� ��ǥ�� ����
            enterImage.transform.position = screenPos;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �÷��̾ Quest ������Ʈ���� �����
        if (other.CompareTag("Player")) {
            enterImage.gameObject.SetActive(false);  // �̹��� ��Ȱ��ȭ
        }
    }
}
