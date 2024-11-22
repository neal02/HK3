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
    public float flip;

    void Start()
    {
        Application.targetFrameRate = 60;
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (rigid2D.velocity.normalized.x < 0)  //�¿����
        {
            spriteRenderer.flipX = true;
            flip = -1;
        }
        else if (rigid2D.velocity.normalized.x > 0)
        {
            spriteRenderer.flipX = false;
            flip = 1;
        }

        float horizontal = Input.GetAxis("Horizontal");
        rigid2D.velocity = new Vector2(horizontal * moveSpeed, rigid2D.velocity.y);

        if (horizontal == 0) //�޸���
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

        if (rigid2D.velocity.normalized.y == 0) //�����ߴ°�
        {
            animator.SetBool("isJump", false);
            maxjump = 2;
        }

        if (Input.GetKeyDown(KeyCode.Z) && isDashCool)    //�뽬
        {
            rigid2D.AddForce(Vector2.right * dashSpeed*flip, ForceMode2D.Impulse); 
            dashcon = -0.5f;
            animator.SetFloat("isDash", dashcon);   //�뽬 �ִϸ��̼��� ��� ��µ� �� �ֵ��� �뽬���� -0.5����Ʈ ������ �ϰ� �̰� 0 ���϶� �ִϸ��̼� ���. 
            //transform.position += new Vector3(dashSpeed * Input.GetAxisRaw("Horizontal"), 0, 0);
            isDashCool = false;
            Debug.Log("�������� ��");
            StartCoroutine(CooldownDash());
        }
        else if (Input.GetKeyDown(KeyCode.X) && maxAttack > 0 && isAttackCool && AttackDelay)   //����
        {
            firstattackcon = -0.5f;
            maxAttack--;
            animator.SetFloat("isAttack", firstattackcon);
            AttackDelay = false;
            Debug.Log("����");
            StartCoroutine(AttDelay());
        }
        else if (Input.GetKeyDown(KeyCode.C) && maxjump > 0)   //����
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("EnemyAttack")) {
            animator.SetTrigger("Hit");
            Debug.Log("�ѵ���");

        }
    }

    IEnumerator CooldownDash()
    {
        yield return new WaitForSeconds(DashcoolTime);
        isDashCool = true;
        Debug.Log("�뽬��밡��");
    }

    IEnumerator AttDelay()
    {
        yield return new WaitForSeconds(0.3f);
        isAttackCool = true;
        maxAttack = 1;
        AttackDelay = true;
        
    }
    public void StopMovement()
    {
        rigid2D.velocity = Vector2.zero;  // Rigidbody�� �ӵ��� 0���� ����
    }

    // �̵��� �簳�ϴ� �Լ�
    public void ResumeMovement()
    {
        rigid2D.velocity = new Vector2(0, 0); // ������� �ӵ� ���� (Ȥ�� ���� �ӵ��� ����)
    }
}