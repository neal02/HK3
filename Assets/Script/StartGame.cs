using UnityEngine;
using UnityEngine.SceneManagement;  // �� �Ŵ��� ���� ���

public class StartGame : MonoBehaviour
{
    void Update()
    {
        // �ƹ� Ű�� ������ ��
        if (Input.anyKeyDown)
        {
            // ���� ������ �̵� (�� �̸� ���)
            SceneManager.LoadScene("2.tutorial&UI");
        }
    }
}
