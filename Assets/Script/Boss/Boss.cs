using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // 보스 스텟
    public float moveSpeed;
    public float bossHp;
    public float currentHp;
    // 컴포넌트
    private Rigidbody2D rigid;
    private Animator anim;
    public GameObject player;
    private Collider2D playerCollider;
    private Collider2D[] colliders;
    private SpriteRenderer sprite;
    // 충돌 관련
    private bool isGrounded;
    // 애니메이션 관련
    //private bool isMoving;
    private bool isFlipping = false;
    private bool isAttacking = false;
    //감지 관련
    private bool isDetecting;
    //거리 관련

    //UI 관련
    public Image hpBarImage;


    

    void Start()
    {
        moveSpeed = 1.5f;
        bossHp = 100.0f;
        currentHp = bossHp;
        UpdateHpBar();

        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("player");
        isGrounded = false;
        isDetecting = false;

        Collider2D playerCollider = player.GetComponent<Collider2D>();
        colliders = GetComponents<Collider2D>();

        Physics2D.IgnoreCollision(colliders[0], playerCollider, true);
    }

    void FixedUpdate()
    {
        if(!isDetecting)
        {
            //RandomMoving(); 발견 못했을 때 무작위 움직임

        }
        else if (isDetecting)
        {
            if(!isAttacking)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    if (!isFlipping) // 이미 지연이 진행 중이면 중복 실행 방지
                    {
                        StartCoroutine(FlipBoss(true));
                    }
                }
                else if (player.transform.position.x > transform.position.x)
                {
                    if (!isFlipping) // 이미 지연이 진행 중이면 중복 실행 방지
                    {
                        StartCoroutine(FlipBoss(false));
                    }
                }
            }
        
            if (isGrounded && isDetecting && !isAttacking)
            {
                Move();
            }
        }
        
    }

    public void UpdateHpBar()
    {
        hpBarImage.fillAmount = currentHp / bossHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, bossHp);
        UpdateHpBar();
    }

    void Move()
    {
        if (!isAttacking)
        {   
            anim.SetBool("isMoving", true);
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed / 50);
            float distance = Mathf.Abs(transform.position.x - targetPosition.x);

            if (distance <= 5)
            {
                isAttacking = true;
                anim.SetBool("isAttacking", true);
                Attack();
                return;
            }
            else
            {
                isAttacking = false;
                anim.SetBool("isAttacking", false);
            }
        } 
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            //isMoving = true;        
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !isDetecting)
            {
                isDetecting = true;
                Debug.Log("플레이어 감지함");
            }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && isDetecting && other.CompareTag("Player")) //어택이나 대쉬가 나갔을 때 풀리면 안됨
        {
            isDetecting = false;
            Debug.Log("플레이어 벗어남");
        }
    }

    IEnumerator FlipBoss(bool flipLeft)
    {
        isFlipping = true;

        if (isAttacking)
        {
            isFlipping = false;
            yield break;
        }

        yield return new WaitForSeconds(1.2f);
        if (flipLeft)
        {
            sprite.flipX = true;  
        }
        else
        {
            sprite.flipX = false;   
        }
        isFlipping = false;
    }

    void Attack()
    {
        if(anim.GetBool("isAttacking"))
        {
            anim.SetBool("isMoving", false);  // 공격 시 이동 애니메이션 비활성화
            // 공격이 끝난 후 이동으로 돌아가게 하려면
            StartCoroutine(EndAttack());  // 공격 끝날 때까지 기다리기
        }
    }

    IEnumerator EndAttack()
    {
        // 공격 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(1.85f); // 애니메이션 지속 시간 (애니메이션 길이에 맞게 조정)
    
        // 애니메이션이 끝나면 이동을 다시 시작
        isAttacking = false;
        anim.SetBool("isAttacking", false);
    }
}
