using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boss : MonoBehaviour //보스의 본체 스크립트, 본체 스크립트에선 보스의 에니메이션과 변수, 함수, 그리고 충돌 콜라이더 구현, 다른 기믹에 대한 구현은 자식 오브젝트에서 함
{

    private Rigidbody2D rigid, playerRigid;
    public Animator anim;
    public GameObject player;
    private Collider2D playerCollider, bossCollider;
    private SpriteRenderer sprite;
    public GameObject jumpAttackTriggerObject;
    public GameObject attackTriggerObject;
    public GameObject thrustTriggerObject;
    public GameObject poisonTriggerObject;

    public AudioSource[] bossAudioSources;

    Thrust_Trigger thrustTrigger;
    playerControl playerScript;

    public enum BossState { idle, attack, jumpAttack, move, thrust, poison, death, exit }
    public BossState currentState = BossState.idle;

    public List<Transform> triggers;

    public bool isDetecting;
    public bool isFirstDetecting;
    public bool isAlive;

    public bool isAttacking = false;
    public bool isJumpAttacking = false;
    public bool isMoving = false;
    public bool isAttackSpliting = false;
    public bool isThrusting = false;
    public bool isPoison = false;
    private bool isFlippingBlocked = false;

    public float moveSpeed;
    public float hp;
    public float attackCooldownDuration = 1.5f;
    public float attackSplitCooldownDuration = 3.0f;
    private bool isFacingRight = true;

    private float patternChangeTime = 2f;

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
        jumpAttackTriggerObject.SetActive(false);
        poisonTriggerObject.SetActive(false);
        playerRigid = player.GetComponent<Rigidbody2D>();
        bossAudioSources = GetComponents<AudioSource>();


        isDetecting = false;
        isFirstDetecting = false;
        isAlive = true;

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Trigger"))
            {
                triggers.Add(child);
            }
        }

        thrustTrigger = thrustTriggerObject.GetComponent<Thrust_Trigger>();
        playerScript = player.GetComponent<playerControl>();

        moveSpeed = 10.0f;
        hp = 10.0f;

        StartCoroutine(RandomPatternRoutine());
    }

    IEnumerator RandomPatternRoutine()
    {
        while (isAlive)
        {
            // 일정 시간마다 랜덤으로 패턴을 선택
            yield return new WaitForSeconds(patternChangeTime);

            if (OtherAct()) continue;

            // 랜덤으로 4개 패턴 중 하나를 선택
            int pattern = Random.Range(0, 5);

            switch (pattern)
            {
                case 0:
                    ChangeState(BossState.attack);
                    break;
                case 1:
                    ChangeState(BossState.jumpAttack);
                    break;
                case 2:
                    ChangeState(BossState.thrust);
                    break;
                case 3:
                    ChangeState(BossState.poison);
                    break;
                case 4:
                    ChangeState(BossState.move);
                    break;
            }
        }
    }

    void StateMachine()
    {
        switch (currentState)
        {
            case BossState.idle:
                Idle();
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
            case BossState.poison:
                StartCoroutine(Poison());
                break;
            case BossState.move:
                StartCoroutine(Move());
                break;
            case BossState.death:
                hp = 1;
                Death();
                break;
            case BossState.exit:
                isAlive = false;
                break;
        }  
    }

    void Idle()
    {
        CheckFliping();

        if (!isFirstDetecting) return;
    }

    void Death()
    {
        bossAudioSources[4].Play();
        anim.SetTrigger("die");
        StartCoroutine(DeathSequence());   
    }

    void Update()
    {
        if (isAlive)
        {
            StateMachine(); 
        }

        if (hp <= 0)
        {
            isAlive = false;
            ChangeState(BossState.death);
            StateMachine();
        }
    }

    IEnumerator Move()
    {
        // 다른 행동 중이면 이동하지 않도록 방지
        if (OtherAct()) yield break;

        // 이동 시작
        anim.SetBool("isMoving", true);
        isMoving = true;

        // 플레이어와 보스의 위치 계산
        Vector3 playerPosition = player.transform.position;
        Vector3 bossPosition = transform.position;

        // 플레이어 방향으로 벡터 계산
        Vector3 direction = new Vector3(playerPosition.x - bossPosition.x, transform.position.y, 0).normalized;

        float moveTime = 1.2f;  // 이동 시간
        float elapsedTime = 0f;

        // 2.4초 동안 플레이어에게 이동
        while (elapsedTime < moveTime)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;

            // 이동 중 애니메이션 갱신
            yield return null;
        }

        // 이동 완료 후
        anim.SetBool("isMoving", false);
        isMoving = false;

        // Idle 상태로 전환
        ChangeState(BossState.idle);
    }


    IEnumerator Attack()
    {
        if (OtherAct()) yield break;

        anim.SetBool("isAttacking", true);
        isAttacking = true;

        isFlippingBlocked = true;

        bossAudioSources[2].Play();

        yield return new WaitForSeconds(1.9f);

        isFlippingBlocked = false;

        anim.SetBool("isAttacking", false);
        isAttacking = false;

        ChangeState(BossState.idle);
    }

    IEnumerator Poison()
    {
        if (OtherAct()) yield break;

        anim.SetBool("isPoison", true);
        isPoison = true;

        isFlippingBlocked = true;

        bossAudioSources[1].Play();

        yield return new WaitForSeconds(1.6f);

        isFlippingBlocked = false;

        anim.SetBool("isPoison", false);
        isPoison = false;

        ChangeState(BossState.idle);
    }

    IEnumerator ActivateJPoisonTrigger()
    {
        poisonTriggerObject.SetActive(true);

        yield return new WaitForSeconds(1.1f);

        poisonTriggerObject.SetActive(false);
    }

    void ChangeState(BossState newState)
    {
        currentState = newState;
    }

    

    void CheckFliping()
    {
        if (isFlippingBlocked) return; // 애니메이션 진행 중에는 플립 금지

        // 플레이어의 위치에 따라 플립 필요 여부 확인
        bool shouldFlip = player.transform.position.x < transform.position.x;

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

                // X축 스케일 반전 (필요한 경우만)
                Vector3 localScale = trigger.localScale;
                localScale.x *= -1;
                trigger.localScale = localScale;
            }
        }
    }

    
    IEnumerator JumpAttack()
    {
        if (OtherAct()) yield break;

        isJumpAttacking = true;

        anim.SetTrigger("jumpUp");

        isFlippingBlocked = true;

        bossAudioSources[3].Play();

        yield return new WaitForSeconds(1.6f);

        bossAudioSources[3].Play();

        anim.SetTrigger("UptoDown");
        Vector3 teleportPosition = new Vector3(player.transform.position.x, -7f, 0);

        sprite.enabled = false;
        bossCollider.enabled = false;

        transform.position = teleportPosition;

        sprite.enabled = true;
        bossCollider.enabled = true;
        yield return new WaitForSeconds(0.9f);

        anim.SetTrigger("jumpDown");

        isFlippingBlocked = false;
        CheckFliping();
        isJumpAttacking = false;
        ChangeState(BossState.idle);
    }

    IEnumerator ActivateJumpAttackTrigger()
    {
        jumpAttackTriggerObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        jumpAttackTriggerObject.SetActive(false);
    }

    bool OtherAct()
    {
        return isMoving || isAttacking || isThrusting || isJumpAttacking || isPoison;
    }

    IEnumerator Thrust()
    {
        if (OtherAct()) yield break;

        //if (!thrustTrigger.isInTrigger) yield break;

        isThrusting = true;

        anim.SetBool("isThrusting", true);

        isFlippingBlocked = true;

        bossAudioSources[5].Play();

        yield return new WaitForSeconds(1.6f);

        isFlippingBlocked = false;
        
        anim.SetBool("isThrusting", false);

        isThrusting = false;
        CheckFliping();
        ChangeState(BossState.idle);
    }

    void PushPlayer(float force)
    {
        if (playerRigid != null)
        {
            Vector2 pushDirection; // 왼쪽으로 밀기
            float pushForce = force;
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

    void PushPlayerY(float force)
    {
        playerRigid.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    /*IEnumerator AttackSplit()
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
    }*/

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
            
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            
        }
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(1.5f);
        anim.enabled = false;
        gameObject.SetActive(false);
        ChangeState(BossState.exit);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Attack")
        {
            //StartCoroutine(AttackedAnimation());
            hp -= 10;
            Debug.Log("현재 체력: " + hp);
        }
    }

    /*IEnumerator AttackedAnimation()
    {
        anim.SetTrigger("isAttacked");
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("attackedReturn");
        
    }*/

    
}

