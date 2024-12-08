using UnityEngine;
using UnityEngine.SceneManagement;  // 씬 매니저 관련 기능

public class NextScene : MonoBehaviour
{
    void Update()
    {
        // 아무 키나 눌렀을 때
        if (Input.anyKeyDown)
        {
            // 다음 씬으로 이동 (씬 이름 사용)
            SceneManager.LoadScene("TutoScene");
        }
    }
}
