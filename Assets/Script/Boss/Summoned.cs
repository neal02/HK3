using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoned : MonoBehaviour
{
    public bool isFlippingBlocked = false;
    public GameObject player;
    public bool isFacingRight = true;
    public SpriteRenderer sprite;
    public List<Transform> triggers;

    void Start()
    {
        player = GameObject.Find("player");
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckFliping();
    }

    void CheckFliping()
    {
        if (isFlippingBlocked) return; // 애니메이션 진행 중에는 플립 금지

        // 플레이어의 위치에 따라 플립 필요 여부 확인
        bool shouldFlip = player.transform.position.x > transform.position.x;

        // 현재 상태와 반대면 플립
        if (shouldFlip != isFacingRight)
        {
            isFacingRight = shouldFlip; // 방향 업데이트
            sprite.flipX = shouldFlip; // 스프라이트 반전

            // 트리거 위치 및 크기 업데이트
            foreach (Transform trigger in triggers)
            {
                Vector3 localPosition = trigger.localPosition;

                // X축 위치 반전
                localPosition.x *= -1;
                trigger.localPosition = localPosition;
            }
        }
    }

    public IEnumerator Summon()
    {
        Vector3 randomPosition = GetRandomPosition();
        transform.position = randomPosition;

        isFlippingBlocked = true;
        gameObject.SetActive(true);

        // 플레이어의 위치에 따라 플립 필요 여부 확인
        bool shouldFlip = player.transform.position.x > transform.position.x;

        // 현재 상태와 반대면 플립
        if (shouldFlip != isFacingRight)
        {
            isFacingRight = shouldFlip; // 방향 업데이트
            sprite.flipX = shouldFlip; // 스프라이트 반전

            // 트리거 위치 및 크기 업데이트
            foreach (Transform trigger in triggers)
            {
                Vector3 localPosition = trigger.localPosition;

                // X축 위치 반전
                localPosition.x *= -1;
                trigger.localPosition = localPosition;
            }
        }

        yield return new WaitForSeconds(2.8f);

        isFlippingBlocked = false;
        gameObject.SetActive(false);
    }

    public Vector3 GetRandomPosition()
    {
        // 보스 위치를 기준으로 랜덤 범위 설정
        float randomX = Random.Range(-4f, 41f); // X축 범위
        float randomY = Random.Range(-14f, -10f); // Y축 범위

        return new Vector3(randomX, randomY, 0);
    }
}
