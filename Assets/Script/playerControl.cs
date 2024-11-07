using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpSpeed = 5.0f;
    public float dashSpeed = 10.0f;
    public float DashcoolTime = 3.0f;
    public float AttackcoolTime = 1.5f;

    bool isDashCool = true;
    bool isAttackCool = true;
    bool AttackDelay = true;

    int maxjump = 2;
    int maxAttack = 1;

    private float dashcon = -0.5f;
    private float firstattackcon = -0.5f;

    Rigidbody2D rigid2D;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Start()
    {
        Application.targetFrameRate = 60;
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (rigid2D.velocity.normalized.x < 0)  //좌우반전
        {
            spriteRenderer.flipX = true;
        }
        else if (rigid2D.velocity.normalized.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        float horizontal = Input.GetAxis("Horizontal");
        rigid2D.velocity = new Vector2(horizontal * moveSpeed, rigid2D.velocity.y);

        if (horizontal == 0) //달리기
        {
            animator.SetBool("isRun", false);
        }
        else if (animator.GetBool("isJump"))
        {
            animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isRun", true);
        }

        if (rigid2D.velocity.normalized.y == 0) //착지했는가
        {
            animator.SetBool("isJump", false);
            maxjump = 2;
        }

        if (Input.GetKeyDown(KeyCode.Z) && isDashCool)    //대쉬
        {
            dashcon = -0.5f;
            animator.SetFloat("isDash", dashcon);   //대쉬 애니매이션이 모두 출력될 수 있도록 대쉬콘을 -0.5디폴트 값으로 하고 이게 0 밑일때 애니매이션 출력. 
            transform.position += new Vector3(dashSpeed * Input.GetAxisRaw("Horizontal"), 0, 0);
            isDashCool = false;
            Debug.Log("이제부터 쿨");
            StartCoroutine(CooldownDash());
        }
        else if (Input.GetKeyDown(KeyCode.X) && maxAttack > 0 && isAttackCool && AttackDelay)   //공격
        {
            firstattackcon = -0.5f;
            maxAttack--;
            animator.SetFloat("isAttack", firstattackcon);
            AttackDelay = false;
            Debug.Log("공격");
            StartCoroutine(AttDelay());
        }
        else if (Input.GetKeyDown(KeyCode.C) && maxjump > 0)   //점프
        {
            maxjump--;
            animator.SetBool("isJump", true);
            animator.SetBool("isRun", false);
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpSpeed);
            animator.SetBool("isFall", false);
        }

        if (animator.GetFloat("isDash") < 1)
        {
            dashcon += 0.03f;
            animator.SetFloat("isDash", dashcon);
        }
        if (animator.GetFloat("isAttack") < 1)
        {
            firstattackcon += 0.03f;
            animator.SetFloat("isAttack", firstattackcon);
        }
    }

    IEnumerator CooldownDash()
    {
        yield return new WaitForSeconds(DashcoolTime);
        isDashCool = true;
        Debug.Log("대쉬사용가능");
    }

    IEnumerator AttDelay()
    {
        yield return new WaitForSeconds(0.3f);
        isAttackCool = true;
        maxAttack = 1;
        AttackDelay = true;
        
    }
}