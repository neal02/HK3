using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EndBC : MonoBehaviour
{
    CapsuleCollider2D CapsuleCollider2D;
    BoxCollider2D BoxCollider2D;
    Rigidbody2D rigid2D;
    SpriteRenderer spriteRenderer;
    GameObject playerflip;
    public Transform player;         // 플레이어의 Transform
    public float teleportInterval = 3f; // 순간이동 간격 (초 단위)
    public float teleportDistance = 1.5f; // 플레이어와 떨어진 거리
    public float Bossflip;
    Animator animator;
    Vector2 colider = new Vector2(0.25f, 0);

    private void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Application.targetFrameRate = 60;
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerflip= GameObject.Find("player");
        // 코루틴 시작
        StartCoroutine(StartTeleportAfterDelay(2f));
    }

    private IEnumerator StartTeleportAfterDelay(float delay)
    {
        // 지정한 초기 지연 시간 대기
        yield return new WaitForSeconds(delay);

        // 이후, 순간이동 코루틴 시작
        StartCoroutine(TeleportBehindPlayer());
    }

    private IEnumerator TeleportBehindPlayer()
    {
        while (true) {
            // 3초 대기
            yield return new WaitForSeconds(teleportInterval);

            animator.SetTrigger("Attack");
            // 플레이어가 바라보는 방향 계산 (1: 오른쪽, -1: 왼쪽)
            float playerDirection = playerflip.GetComponent<playerControl>().flip;
            if (playerDirection ==1f) {
                Vector2 teleportPosition = new Vector2(
                    player.position.x - playerDirection * teleportDistance+1.5f,
                    player.position.y - 4.5f
                );
            transform.position = teleportPosition;
            spriteRenderer.flipX = true;
            Bossflip = -1;

            }
            else {
                Vector2 teleportPosition = new Vector2(
                    player.position.x - playerDirection * teleportDistance -1.5f,
                    player.position.y - 4.5f
                    
                );
            transform.position = teleportPosition;
            spriteRenderer.flipX = false;
            Bossflip = 1;

            }

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Bossflip == 1)  // 보스가 좌우 반전되었을 때
       {
            BoxCollider2D.offset = new Vector2(-0.25f, 0.95f);

            //CapsuleCollider2D.enabled = false


        }
        else if (Bossflip == -1) {
            BoxCollider2D.offset = new Vector2(0.25f, 0.95f);

            //CapsuleCollider2D.enabled = true;

        }

    }
}

