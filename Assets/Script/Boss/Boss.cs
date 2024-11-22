using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boss : MonoBehaviour //보스의 본체 스크립트, 본체 스크립트에선 보스의 에니메이션과 변수, 함수, 그리고 충돌 콜라이더 구현, 다른 기믹에 대한 구현은 자식 오브젝트에서 함
{

    private Rigidbody2D rigid, playerRigid;
    public Animator anim;
    public GameObject player;
    private Collider2D playerCollider, bossCollider;
    private SpriteRenderer sprite;

    public GameObject jumpAttackTrigger;
    public GameObject attackTrigger;
    public GameObject thrustTrigger;
    Attack_Trigger attackTriggerScript;
    Thrust_Trigger thrustTriggerScript;

    public enum BossState { idle, move, attack, jumpAttack, attackSplit, thrust, death, exit }
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
    private bool isGrounded = false;

    public float moveSpeed;
    public float hp;
    private float attackCooldown = 0f;
    public float attackCooldownDuration = 1.5f;
    public float attackSplitCooldownDuration = 3.0f;

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
        thrustTriggerScript = thrustTrigger.GetComponent<Thrust_Trigger>();
        playerRigid = player.GetComponent<Rigidbody2D>();

        isDetecting = false;
        isFirstDetecting = false;
        isAlive = true;
        
        moveSpeed = 2.0f;
        hp = 10.0f;
        StartCoroutine(StateMachine());
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
                    StartCoroutine(Thrust());
                    break;
                case BossState.attackSplit:
                    StartCoroutine(AttackSplit());
                    break;
                case BossState.death:
                    Death();
                    break;
                case BossState.exit:
                    isAlive = false;
                    break;
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

    void Death()
    {
        anim.SetBool("isDied", true);
        StartCoroutine(DeathSequence());   
    }

    void Update()
    {
        if(hp <= 0)
        {
            ChangeState(BossState.death);
            hp = 1;
        }

        if(currentState != BossState.exit || currentState != BossState.thrust)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 bossPosition = transform.position;

            Vector3 direction = new Vector3((playerPosition.x - bossPosition.x), 0, 0);

            if (attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }

            if(isGrounded && isMoving)
            {
                transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            }

        } 
    }

    void MoveToPlayer()
    {
        anim.SetBool("isStanding", false);
        anim.SetBool("isMoving", true);   
        isMoving = true;
        CheckFliping();

        if(isDetecting)
        {
            if (isThrusting)
            {
                return;
            }

            if(math.abs(player.transform.position.x - transform.position.x) <= 5.0f)
                {
                    attackTrigger.SetActive(true);
                    attackTriggerScript.isDetecting = true;
                    if(attackTriggerScript.isDetecting)
                    {
                        if(attackCooldown <= 0)
                        {
                            anim.SetBool("isMoving", false);
                            anim.SetBool("isStanding", true);
                            isMoving = false;

                            attackCooldown = attackCooldownDuration;
                            ChangeState(BossState.attack);
                        } 
                    }
                }
            /*else
            {
                if(math.abs(player.transform.position.x - transform.position.x) <= 10.0f)
                {
                    if(attackTriggerScript.isDetecting)
                    {
                        if(attackCooldown <= 0)
                        {
                            anim.SetBool("isMoving", false);
                            anim.SetBool("isStanding", true);
                            isMoving = false;

                            attackCooldown = attackSplitCooldownDuration;
                            ChangeState(BossState.attackSplit);

                        } 
                    }
                }    
            }*/

            if(thrustTriggerScript.isInTrigger)
            {
                anim.SetBool("isMoving", false);
                anim.SetBool("isThrusting", true);
                isMoving = false;
                ChangeState(BossState.thrust);
                return;
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
    }

    void ChangeState(BossState newState)
    {
        currentState = newState;

        if(newState == BossState.exit)
        {
            // 보스 사망 시 호출
            FindObjectOfType<SceneTransition>().EndBattleAndFadeOut("ClearScene");

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

        yield return new WaitForSeconds(0.7f);

        anim.SetBool("isStanding", true);
        anim.SetBool("isJumpAttackLand", false);
        anim.SetTrigger("moveTrigger"); 
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
        if (OtherAct() || isThrusting) 
        {
            yield break;
        }
                
        if(!thrustTriggerScript.isInTrigger)
        {
            yield break;
        }

        thrustTriggerScript.isInTrigger = false;
        isThrusting = true;

        anim.SetBool("isMoving", false);
        anim.SetBool("isStanding", false);
        anim.SetBool("isThrusting", true);
        anim.SetTrigger("thrustTrigger");

        isFlippingBlocked = true;

        yield return new WaitForSeconds(1.1f);

        PushPlayer();

        yield return new WaitForSeconds(0.2f);

        isFlippingBlocked = false;
        isThrusting = false;
        CheckFliping();

        anim.SetBool("isThrusting", false);
        anim.SetBool("isStanding", true);

        ChangeState(BossState.idle);
    }

    void PushPlayer()
    {
        if (playerRigid != null)
        {
            Vector2 pushDirection; // 왼쪽으로 밀기
            float pushForce = 2000f; // 힘의 크기
            float pushDuration = 0.5f; // 밀리는 지속 시간

            if (player.transform.position.x < transform.position.x)
            {
                pushDirection = new Vector2(-1, 0);
                StartCoroutine(PushPlayerSmoothly(playerRigid, pushDirection, pushForce, pushDuration));

            }
            else if (player.transform.position.x > transform.position.x)
            {
                pushDirection = new Vector2(1, 0);
                StartCoroutine(PushPlayerSmoothly(playerRigid, pushDirection, pushForce, pushDuration));
            }    
        }
    }

    IEnumerator AttackSplit()
    {
        if (OtherAct() || isAttackSpliting) 
        {
            yield break;
        }

        isAttackSpliting = true;

        anim.SetBool("isMoving", false);
        anim.SetBool("isStanding", false);
        anim.SetBool("isAttackSpliting", true);

        isFlippingBlocked = true;

        yield return new WaitForSeconds(1.9f);

        // 애니메이션이 끝난 후 플립 재개
        isFlippingBlocked = false;
        CheckFliping();
        Debug.Log("침 뱉기");
        anim.SetBool("isAttackSpliting", false);
        anim.SetBool("isStanding", true);
        isAttackSpliting = false;
        ChangeState(BossState.idle);
    }

    IEnumerator PushPlayerSmoothly(Rigidbody2D playerRigidbody, Vector2 direction, float pushForce, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            playerRigidbody.AddForce(direction * pushForce * Time.deltaTime, ForceMode2D.Impulse);
            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(1.5f);
        anim.enabled = false;
        gameObject.SetActive(false);
        ChangeState(BossState.exit);
    }
}
