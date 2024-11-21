using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 기능

public class BossDoor : MonoBehaviour
{
    public string nextSceneName; // 이동할 씬 이름

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어가 문에 닿았는지 확인
        {
            SceneManager.LoadScene(nextSceneName); // 지정된 씬으로 이동
        }
    }
}
