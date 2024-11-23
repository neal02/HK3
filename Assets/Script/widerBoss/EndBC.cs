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
    public Transform player;         // �÷��̾��� Transform
    public float teleportInterval = 3f; // �����̵� ���� (�� ����)
    public float teleportDistance = 1.5f; // �÷��̾�� ������ �Ÿ�
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
        // �ڷ�ƾ ����
        StartCoroutine(StartTeleportAfterDelay(2f));
    }

    private IEnumerator StartTeleportAfterDelay(float delay)
    {
        // ������ �ʱ� ���� �ð� ���
        yield return new WaitForSeconds(delay);

        // ����, �����̵� �ڷ�ƾ ����
        StartCoroutine(TeleportBehindPlayer());
    }

    private IEnumerator TeleportBehindPlayer()
    {
        while (true) {
            // 3�� ���
            yield return new WaitForSeconds(teleportInterval);

            animator.SetTrigger("Attack");
            // �÷��̾ �ٶ󺸴� ���� ��� (1: ������, -1: ����)
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

        if (Bossflip == 1)  // ������ �¿� �����Ǿ��� ��
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

