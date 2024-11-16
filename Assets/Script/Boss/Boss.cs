using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour //보스의 본체 스크립트, 본체 스크립트에선 보스의 에니메이션과 변수, 함수, 그리고 충돌 콜라이더 구현, 다른 기믹에 대한 구현은 자식 오브젝트에서 함
{

    private Rigidbody2D rigid;
    public Animator anim;
    public GameObject player;
    private Collider2D playerCollider, bossCollider;
    private SpriteRenderer sprite;

    public bool isDetecting;

    public bool isAttacking = false;
    public bool isJumpAttacking = false;
    public bool isMoving = false;
    public bool isAttackSpliting = false;
    private bool isFlippingBlocked = false;

    public float moveSpeed;

    void Start()
    {
        Application.targetFrameRate = 60;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("player");
        playerCollider = player.GetComponent<Collider2D>();
        bossCollider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();

        Physics2D.IgnoreCollision(bossCollider, playerCollider, true);

        moveSpeed = 1.0f;
        isDetecting = false;

        anim.SetBool("isStanding", true);

    }

    void Update()
    {
        CheckFliping();

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(JumpAttack());
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(Attack());
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(AttackSplit());
        }
    }

    void CheckFliping()
    {
        if (isFlippingBlocked) return; // 애니메이션 진행 중일 때는 플립을 하지 않음

        if (player.transform.position.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else if (player.transform.position.x > transform.position.x)
        {
            sprite.flipX = false;
        }
    }
    

    IEnumerator JumpAttack()
    {
        Debug.Log("점프 공격 테스트");

        if (isMoving || isAttacking || isAttackSpliting || isJumpAttacking) 
        {
            Debug.Log("다른 행동 중입니다.");
            yield break;
        }

        isJumpAttacking = true;

        // 초기 상태 설정
        anim.SetBool("isMoving", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isStanding", false);
        anim.SetBool("isJumpAttackUp", true);

        isFlippingBlocked = true; // 애니메이션 진행 중 플립을 차단

        // 1초 대기 후, 플레이어의 좌표를 미리 계산
        yield return new WaitForSeconds(1.0f);

        // 플레이어 위치에 기반한 텔레포트 위치 계산
        Vector3 teleportPosition = new Vector3(player.transform.position.x, -5f, player.transform.position.z);

        // 그 후에 오브젝트를 비활성화
        sprite.enabled = false;
        bossCollider.enabled = false;
        anim.SetBool("isJumpAttackUp", false);

        // 0.6초 대기 후 플레이어 위로 순간이동
        yield return new WaitForSeconds(0.6f);

        // 위치 이동
        transform.position = teleportPosition;

        sprite.enabled = true;
        bossCollider.enabled = true;

        yield return new WaitForSeconds(0.9f);

        // 애니메이션 처리
        anim.SetTrigger("landTrigger");

        // 1.0초 대기 (land 애니메이션이 끝날 때까지 기다림)
        yield return new WaitForSeconds(1.0f);

        // 애니메이션이 끝난 후 플립 재개
        isFlippingBlocked = false;
        CheckFliping();

        // 마지막 상태 전환
        anim.SetBool("isStanding", true);
        isJumpAttacking = false;
    }


    IEnumerator Attack()
    {
        Debug.Log("기본 공격 테스트");

        if (isMoving || isAttacking || isAttackSpliting || isJumpAttacking) 
        {
            Debug.Log("다른 행동 중입니다.");
            yield break;
        }

        isAttacking = true;

        anim.SetBool("isMoving", false);
        anim.SetBool("isStanding", false);
        anim.SetBool("isAttacking", true);

        isFlippingBlocked = true; // 애니메이션 진행 중 플립을 차단

        // 1.9초 대기
        yield return new WaitForSeconds(1.9f);
        // GetDamage(damage);

        // 애니메이션이 끝난 후 플립 재개
        isFlippingBlocked = false;
        CheckFliping();

        anim.SetBool("isAttacking", false);
        anim.SetBool("isStanding", true);
        isAttacking = false;
    }

    IEnumerator AttackSplit()
    {
        Debug.Log("기본 공격 테스트");

        if (isMoving || isAttacking || isAttackSpliting || isJumpAttacking) 
        {
            Debug.Log("다른 행동 중입니다.");
            yield break;
        }

        isAttackSpliting = true;

        anim.SetBool("isMoving", false);
        anim.SetBool("isStanding", false);
        anim.SetBool("isAttackSpliting", true);

        isFlippingBlocked = true; // 애니메이션 진행 중 플립을 차단

        // 1.9초 대기
        yield return new WaitForSeconds(1.9f);
        // GetDamage(damage);

        // 애니메이션이 끝난 후 플립 재개
        isFlippingBlocked = false;
        CheckFliping();

        anim.SetBool("isAttackSpliting", false);
        anim.SetBool("isStanding", true);
        isAttackSpliting = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {

    }
}
