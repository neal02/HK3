using UnityEngine;
using UnityEngine.UI;  // UI 기능을 사용하기 위해 필요

public class QuestTrigger : MonoBehaviour
{
    public GameObject player;  // 플레이어 객체
    public Image enterImage;   // UI 이미지로 표시될 "Enter" 이미지

    private void Start()
    {
        // 처음에는 "Enter" 이미지를 비활성화
        enterImage.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 Quest 오브젝트에 닿으면
        if (other.CompareTag("Player")) {
            enterImage.gameObject.SetActive(true);  // 이미지 활성화

            // 플레이어의 월드 좌표를 화면 좌표로 변환
            Vector3 playerPosition = player.transform.position;
            playerPosition.y += 1.8f; // 머리 위 위치 (y=1f를 더하여 머리 위로 이동)

            // 월드 좌표를 화면 좌표로 변환
            Vector3 screenPos = Camera.main.WorldToScreenPoint(playerPosition);

            // 이미지 위치를 화면 좌표로 설정
            enterImage.transform.position = screenPos;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 Quest 오브젝트에서 벗어나면
        if (other.CompareTag("Player")) {
            enterImage.gameObject.SetActive(false);  // 이미지 비활성화
        }
    }
}
