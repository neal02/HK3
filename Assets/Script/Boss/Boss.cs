using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Boss : MonoBehaviour //보스의 본체 스크립트, 본체 스크립트에선 보스의 에니메이션과 변수, 함수, 그리고 충돌 콜라이더 구현, 다른 기믹에 대한 구현은 자식 오브젝트에서 함
{

    private Rigidbody2D rigid;
    public Animator anim;
    public GameObject player;
    private Collider2D playerCollider, bossCollider;
    private SpriteRenderer sprite;

    public GameObject jumpAttackTrigger;
    public GameObject attackTrigger;
    Attack_Trigger attackTriggerScript;

    public enum BossState { idle, move, attack, jumpAttack, attackSplit, thrust }
    public BossState currentState = BossState.idle;

    public bool isDetecting;
    public bool isFirstDetecting;
    private bool isAlive;

    public bool isAttacking = false;
    public bool isJumpAttacking = false;
    public bool isMoving = false;
    public bool isAttackSpliting = false;
    public bool isThrusting = false;
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
        jumpAttackTrigger.SetActive(false);
        attackTriggerScript = attackTrigger.GetComponent<Attack_Trigger>();
        
        moveSpeed = 2.0f;
        isDetecting = false;
        isFirstDetecting = false;
        isAlive = true;

        StartCoroutine(StateMachine());

        anim.SetBool("isStanding", true);

    }

    IEnumerator StateMachine()
    {
        while (isAlive)
        {
            switch (currentState)
            {
                case BossState.idle:
                    Idle();
                    break;
                case BossState.move:
                    MoveToPlayer();
                    break;
                case BossState.attack:
                    StartCoroutine(Attack());
                    break;
                case BossState.jumpAttack:
                    StartCoroutine(JumpAttack());
                    break;
                case BossState.thrust:
                    break;
                /*case BossState.death:
                    Death();
                    break;*/
            }
            yield return null;
        }
    }

    void Idle()
    {
        anim.SetBool("isStanding", true);
        if (isFirstDetecting)
        {
            ChangeState(BossState.move);
        }
    }

    void MoveToPlayer()
    {
        anim.SetBool("isStanding", false);
        anim.Play("move");   
        isMoving = true;

        Vector3 playerPosition = player.transform.position;
        Vector3 bossPosition = transform.position;

        Vector3 direction = new Vector3((playerPosition.x - bossPosition.x), 0, 0);

        if(isDetecting)
        {
            anim.SetBool("isMoving", true);

            if(attackTriggerScript.isDetecting)
            {
                anim.SetBool("isMoving", false);
                anim.SetBool("isStanding", true);
                isMoving = false;
                ChangeState(BossState.attack);
            }

            if(math.abs(direction.x) >= 0.2f)
            {
                transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isStanding", true);
            anim.Play("jump_attack_up");
            isMoving = false;
            ChangeState(BossState.jumpAttack);    
        }

        CheckFliping();
    }

    void ChangeState(BossState newState)
    {
        currentState = newState;
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
    
    bool OtherAct()
    {
        if (isThrusting || isMoving || isAttacking || isAttackSpliting || isJumpAttacking)
            return true;
        else
            return false;
    }
    IEnumerator JumpAttack()
    {
        if (OtherAct()) 
        {
            yield break;
        }

        isJumpAttacking = true;

        anim.SetBool("isMoving", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isStanding", false);
        anim.SetBool("isJumpAttackUp", true);

        isFlippingBlocked = true;

        attackTrigger.SetActive(false);
        attackTriggerScript.attackDamageTrigger.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        Vector3 teleportPosition = new Vector3(player.transform.position.x, -5f, player.transform.position.z);

        sprite.enabled = false;
        bossCollider.enabled = false;
        anim.SetBool("isJumpAttackUp", false);

        yield return new WaitForSeconds(0.6f);

        transform.position = teleportPosition;

        sprite.enabled = true;
        bossCollider.enabled = true;

        anim.SetTrigger("landTrigger");

        yield return new WaitForSeconds(0.1f);
        jumpAttackTrigger.SetActive(true);

        yield return new WaitForSeconds(0.9f);

        anim.SetBool("isStanding", true); 
        jumpAttackTrigger.SetActive(false);

        isFlippingBlocked = false;
        CheckFliping();
        isJumpAttacking = false;
        isDetecting = true;
        ChangeState(BossState.idle);
    }

    void ActivateJumpAttackTrigger()
    {
        attackTrigger.SetActive(true);
        attackTriggerScript.attackDamageTrigger.SetActive(true);
    }



    IEnumerator Attack()
    {
        if (OtherAct()) 
        {
            yield break;
        }

        if(!attackTriggerScript.isDetecting)
        {
            yield break;
        }

        isAttacking = true;

        anim.SetBool("isMoving", false);
        anim.SetBool("isStanding", false);
        anim.SetBool("isAttacking", true);

        isFlippingBlocked = true;
        yield return new WaitForSeconds(0.8f);
        attackTriggerScript.attackDamageTriggerScript.isAttacking = true;
        StartCoroutine(attackTriggerScript.attackDamageTriggerScript.GetDamage());

        isFlippingBlocked = false;
        CheckFliping();

        anim.SetBool("isAttacking", false);
        anim.SetBool("isStanding", true);
        isAttacking = false;
        ChangeState(BossState.idle);
    }

    IEnumerator Thrust()
    {
        if (OtherAct()) 
        {
            yield break;
        }

        if(!attackTriggerScript.isDetecting)
        {
            yield break;
        }

        isThrusting = true;

        anim.SetBool("isMoving", false);
        anim.SetBool("isStanding", false);
        anim.SetBool("isThrusting", true);

        isFlippingBlocked = true;
        yield return new WaitForSeconds(0.8f);
        attackTriggerScript.attackDamageTriggerScript.isAttacking = true;
        StartCoroutine(attackTriggerScript.attackDamageTriggerScript.GetDamage());

        isFlippingBlocked = false;
        CheckFliping();

        anim.SetBool("isAttacking", false);
        anim.SetBool("isStanding", true);
        isAttacking = false;
        ChangeState(BossState.idle);
    }
    
    IEnumerator AttackSplit()
    {
        if (OtherAct()) 
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

    void OnCollisionExit2D(Collision2D other)
    {
        
    }
}
