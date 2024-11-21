using UnityEngine;
using UnityEngine.SceneManagement; // �� ���� ���

public class BossDoor : MonoBehaviour
{
    public string nextSceneName; // �̵��� �� �̸�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // �÷��̾ ���� ��Ҵ��� Ȯ��
        {
            SceneManager.LoadScene(nextSceneName); // ������ ������ �̵�
        }
    }
}
