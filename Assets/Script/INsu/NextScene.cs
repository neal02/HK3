using UnityEngine;
using UnityEngine.SceneManagement;  // �� �Ŵ��� ���� ���

public class NextScene : MonoBehaviour
{
    void Update()
    {
        // �ƹ� Ű�� ������ ��
        if (Input.anyKeyDown)
        {
            // ���� ������ �̵� (�� �̸� ���)
            SceneManager.LoadScene("TutoScene");
        }
    }
}
