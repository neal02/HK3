using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DashAttack : MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    [SerializeField] private Image dashIcon; // DashAttack 아이콘 이미지
    [SerializeField] private float cooldownTime = 1.0f; // 쿨타임 시간 설정
    [SerializeField] private Image cooldownOverlay; // 쿨타임 진행을 보여주는 원형 UI

    private bool delay = true;

    void Start()
    {
        Application.targetFrameRate = 60;
        boxCollider2D = GetComponent<BoxCollider2D>();

        if (dashIcon == null || cooldownOverlay == null)
        {
            Debug.LogError("DashAttack 아이콘 또는 쿨타임 오버레이가 설정되지 않았습니다.");
        }

        // 쿨타임 UI 초기화
        cooldownOverlay.fillAmount = 0f; // 시작 시 쿨타임 UI는 비어있음
    }

    void Update()
    {
        // DashAttack의 방향 설정
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            boxCollider2D.offset = new Vector2(Math.Abs(boxCollider2D.offset.x), boxCollider2D.offset.y);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            boxCollider2D.offset = new Vector2(Math.Abs(boxCollider2D.offset.x) * -1, boxCollider2D.offset.y);
        }

        // DashAttack 발동
        if (Input.GetKeyDown(KeyCode.Z) && delay)
        {
            delay = false;
            boxCollider2D.enabled = true;
            boxCollider2D.size = new Vector2(0.59f, 0.09f);
            StartCoroutine(Delay());
        }
        else
        {
            boxCollider2D.enabled = false;
        }
    }

    IEnumerator Delay()
    {
        // 쿨타임 시작
        float elapsedTime = 0f;
        cooldownOverlay.fillAmount = 1f; // 쿨타임 UI를 꽉 채운 상태로 시작
        dashIcon.color = new Color(0.5f, 0.5f, 0.5f, 1f); // 아이콘을 어둡게 설정

        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / cooldownTime;

            // 쿨타임 UI가 점점 줄어듦
            cooldownOverlay.fillAmount = 1f - progress;

            // 아이콘 색상이 점점 밝아짐
            float brightness = Mathf.Lerp(0.5f, 1f, progress);
            dashIcon.color = new Color(brightness, brightness, brightness, 1f);

            yield return null;
        }

        // 쿨타임 종료
        delay = true;
        cooldownOverlay.fillAmount = 0f; // 쿨타임 UI 초기화
        dashIcon.color = new Color(1f, 1f, 1f, 1f); // 아이콘 색상을 완전히 밝게 설정
    }
}
