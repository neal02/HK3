using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;  // �� ��ȯ�� ���� �߰�
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

    // ü�� ���� ����
    public int health = 100;  // �ʱ� ü�� ����
    public int maxHealth = 100;  // �ִ� ü��

    GameDirector gameDirector; // GameDirector ����

    void Start()
    {
        Application.targetFrameRate = 60;
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // GameDirector ������Ʈ ã��
        gameDirector = GameObject.FindObjectOfType<GameDirector>();
    }

    void Update()
    {
        if (rigid2D.velocity.normalized.x < 0)  // �¿����
        {
            spriteRenderer.flipX = true;
        }
        else if (rigid2D.velocity.normalized.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        float horizontal = Input.GetAxis("Horizontal");
        rigid2D.velocity = new Vector2(horizontal * moveSpeed, rigid2D.velocity.y);

        if (horizontal == 0) // �޸���
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

        if (rigid2D.velocity.normalized.y == 0) // �����ߴ°�
        {
            animator.SetBool("isJump", false);
            maxjump = 2;
        }

        if (Input.GetKeyDown(KeyCode.Z) && isDashCool)  // �뽬
        {
            dashcon = -0.5f;
            animator.SetFloat("isDash", dashcon);
            transform.position += new Vector3(dashSpeed * Input.GetAxisRaw("Horizontal"), 0, 0);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.dash);  // AudioManager ���
            isDashCool = false;
            StartCoroutine(CooldownDash());
        }
        else if (Input.GetKeyDown(KeyCode.X) && maxAttack > 0 && isAttackCool && AttackDelay)  // ����
        {
            firstattackcon = -0.5f;
            maxAttack--;
            animator.SetFloat("isAttack", firstattackcon);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Attack);  // AudioManager ���
            AttackDelay = false;
            StartCoroutine(AttDelay());
        }
        else if (Input.GetKeyDown(KeyCode.C) && maxjump > 0)  // ����
        {
            maxjump--;
            animator.SetBool("isJump", true);
            animator.SetBool("isRun", false);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Jump);  // AudioManager ���
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

    public void TakeDamage(int damage)
    {
        health -= damage;  // ü�� ����

        // GameDirector�� ���� ü�� ������ ������Ʈ
        if (gameDirector != null)
        {
            gameDirector.UpdateHpGauge(health, maxHealth);
        }

        if (health <= 0)
        {
            health = 0;
            animator.SetTrigger("death");
            Die();  // ��� ó��
        }
        else
        {
            animator.SetTrigger("hit");  // �´� �ִϸ��̼� ���
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);  // ���� �� ȿ����
        }
    }

    void Die()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Death);  // ��� ȿ����
        Invoke("LoadStartScene", 1f);
    }

    void LoadStartScene()
    {
        SceneManager.LoadScene("boss1");
    }

    IEnumerator CooldownDash()
    {
        yield return new WaitForSeconds(DashcoolTime);
        isDashCool = true;
    }

    IEnumerator AttDelay()
    {
        yield return new WaitForSeconds(0.3f);
        isAttackCool = true;
        maxAttack = 1;
        AttackDelay = true;
    }
}
