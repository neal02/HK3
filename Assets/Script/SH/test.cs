using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    /*public enum BossState { idle, move, attack, hit, heal, death }
    public BossState currentState = BossState.idle;

    public Transform player;
    public float moveSpeed = 1f; // �̵� �ӵ� ���̱� (��: 2f���� 1f��)
    public float attackRange = 2f;
    public float fieldOfVision = 10f;
    public int maxHealth = 100;
    private int currentHealth;
    private bool isAlive = true;
    private bool hasHealed = false; // heal�� �� ���� �����ϵ��� üũ�ϴ� ����

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float attackDelay = 4f; // ���� �ֱ� 3��
    private float nextAttackTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer ��������
        currentHealth = maxHealth;
        StartCoroutine(StateMachine());
    }

    private IEnumerator StateMachine()
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
                    AttackPlayer();
                    break;
                case BossState.hit:
                    yield return StartCoroutine(Hit());  // �ǰ� �ִϸ��̼��� ���� ������ ���
                    break;
                case BossState.heal:
                    Heal();
                    break;
                case BossState.death:
                    Death();
                    break;
            }
            yield return null;
        }
    }

    private void Idle()
    {
        animator.SetBool("moving", false);

        // �þ� ������ �÷��̾ ������ �̵� ���·� ��ȯ
        if (Vector3.Distance(transform.position, player.position) <= fieldOfVision)
        {
            ChangeState(BossState.move);
        }
    }

    private void MoveToPlayer()
    {
        animator.SetBool("moving", true);

        // �÷��̾� �������� �̵�
        float direction = player.position.x - transform.position.x;

        // �÷��̾ �ִ� �������� ������ �ٶ󺸰� ����
        if (direction < 0)
        {
            spriteRenderer.flipX = true; // ����
        }
        else
        {
            spriteRenderer.flipX = false; // ������
        }

        // ������ �̵�
        transform.Translate(new Vector2(direction, 0) * moveSpeed * Time.deltaTime);

        // ���� ������ ������ ���� ���·� ��ȯ
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            ChangeState(BossState.attack);
        }
    }

    private void AttackPlayer()
    {
        // ���� �ִϸ��̼� Ʈ����
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackDelay; // ���� ���� �ð� ����
            animator.SetTrigger("attack");

            // ���� ���� �߰�
            StartCoroutine(AttackHitbox());
        }

        // ���� �� �ٽ� �̵� ���·� ��ȯ
        ChangeState(BossState.move);
    }

    // ���� ���� ����
    private IEnumerator AttackHitbox()
    {
        // ���� �ִϸ��̼��� ����Ǹ鼭 ��� �� ������ Ȱ��ȭ
        yield return new WaitForSeconds(0.2f); // �ִϸ��̼��� ���� ���� Ÿ�ֿ̹� �°� ����

        // �÷��̾ ���� ���� ���� �ִ��� Ȯ��
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player")) // �÷��̾� �±� Ȯ��
            {
                // Player ��ũ��Ʈ�� TakeDamage �޼��� ȣ��
                //hit.GetComponent<playerControl>()?.TakeDamage(10); // ���� ������ 10
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // ���� ���� ǥ��
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isAlive = false;
            ChangeState(BossState.death);
        }
        else if (currentHealth <= maxHealth / 2 && !hasHealed)
        {
            ChangeState(BossState.heal); // ü���� ���� ���Ϸ� �������� heal ���·� ��ȯ
        }
        else
        {
            ChangeState(BossState.hit); // hit �ִϸ��̼� ���
        }
    }

    private IEnumerator Hit()
    {
        animator.SetTrigger("hit");

        // �ǰ� �� �ִϸ��̼��� ���� ������ ���
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // �ִϸ��̼��� ���� �� �̵� ���·� ��ȯ
        ChangeState(BossState.move);
    }

    private void Heal()
    {
        if (!hasHealed)
        {
            animator.SetTrigger("heal");

            // ȸ�� ����, ü�� ȸ��
            currentHealth = Mathf.Min(currentHealth + 20, maxHealth);

            // ȸ�� �� ���� �ʱ�ȭ
            hasHealed = true;
            // �ִϸ��̼��� ���� �� ���¸� �����ϱ� ���� ��� ���
            StartCoroutine(WaitForHealAnimation());
        }
    }

    private IEnumerator WaitForHealAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Heal �ִϸ��̼��� ���� ������ ��ٸ�
        ChangeState(BossState.move); // �ִϸ��̼� ������ �̵� ���·� ��ȯ
    }

    private void Death()
    {
        // �ִϸ������� isAlive �Ķ���͸� false�� ����
        animator.SetBool("isAlive", false);

        // death �ִϸ��̼� Ʈ����
        animator.SetTrigger("death");

        // ���°� ���� ���·� ����
        isAlive = false;

        // �� �̻� ���� ������ �Ͼ�� �ʵ��� ��
    }

    private void ChangeState(BossState newState)
    {
        currentState = newState;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            TakeDamage(10); // ���� ���� �ʿ信 ���� ���� ����
        }
    }*/
}
