using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public enum BossState { idle, move, attack, hit, heal, death }
    public BossState currentState = BossState.idle;

    public Transform player;
    public float moveSpeed = 1f; // 이동 속도 줄이기 (예: 2f에서 1f로)
    public float attackRange = 2f;
    public float fieldOfVision = 10f;
    public int maxHealth = 100;
    private int currentHealth;
    private bool isAlive = true;
    private bool hasHealed = false; // heal을 한 번만 실행하도록 체크하는 변수

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float attackDelay = 4f; // 공격 주기 3초
    private float nextAttackTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer 가져오기
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
                    yield return StartCoroutine(Hit());  // 피격 애니메이션을 끝날 때까지 대기
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

        // 시야 범위에 플레이어가 들어오면 이동 상태로 전환
        if (Vector3.Distance(transform.position, player.position) <= fieldOfVision)
        {
            ChangeState(BossState.move);
        }
    }

    private void MoveToPlayer()
    {
        animator.SetBool("moving", true);

        // 플레이어 방향으로 이동
        float direction = player.position.x - transform.position.x;

        // 플레이어가 있는 방향으로 보스가 바라보게 설정
        if (direction < 0)
        {
            spriteRenderer.flipX = true; // 왼쪽
        }
        else
        {
            spriteRenderer.flipX = false; // 오른쪽
        }

        // 보스가 이동
        transform.Translate(new Vector2(direction, 0) * moveSpeed * Time.deltaTime);

        // 공격 범위에 들어오면 공격 상태로 전환
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            ChangeState(BossState.attack);
        }
    }

    private void AttackPlayer()
    {
        // 공격 애니메이션 트리거
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackDelay; // 다음 공격 시간 갱신
            animator.SetTrigger("attack");

            // 공격 판정 추가
            StartCoroutine(AttackHitbox());
        }

        // 공격 후 다시 이동 상태로 전환
        ChangeState(BossState.move);
    }

    // 공격 판정 생성
    private IEnumerator AttackHitbox()
    {
        // 공격 애니메이션이 실행되면서 잠시 후 판정을 활성화
        yield return new WaitForSeconds(0.2f); // 애니메이션의 실제 공격 타이밍에 맞게 조정

        // 플레이어가 공격 범위 내에 있는지 확인
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player")) // 플레이어 태그 확인
            {
                // Player 스크립트의 TakeDamage 메서드 호출
                hit.GetComponent<playerControl>()?.TakeDamage(10); // 공격 데미지 10
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // 공격 범위 표시
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
            ChangeState(BossState.heal); // 체력이 절반 이하로 떨어지면 heal 상태로 전환
        }
        else
        {
            ChangeState(BossState.hit); // hit 애니메이션 재생
        }
    }

    private IEnumerator Hit()
    {
        animator.SetTrigger("hit");

        // 피격 후 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // 애니메이션이 끝난 후 이동 상태로 전환
        ChangeState(BossState.move);
    }

    private void Heal()
    {
        if (!hasHealed)
        {
            animator.SetTrigger("heal");

            // 회복 로직, 체력 회복
            currentHealth = Mathf.Min(currentHealth + 20, maxHealth);

            // 회복 후 상태 초기화
            hasHealed = true;
            // 애니메이션이 끝날 때 상태를 변경하기 위해 잠시 대기
            StartCoroutine(WaitForHealAnimation());
        }
    }

    private IEnumerator WaitForHealAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Heal 애니메이션이 끝날 때까지 기다림
        ChangeState(BossState.move); // 애니메이션 끝나면 이동 상태로 전환
    }

    private void Death()
    {
        // 애니메이터의 isAlive 파라미터를 false로 설정
        animator.SetBool("isAlive", false);

        // death 애니메이션 트리거
        animator.SetTrigger("death");

        // 상태가 죽음 상태로 고정
        isAlive = false;

        // 더 이상 상태 변경이 일어나지 않도록 함
    }

    private void ChangeState(BossState newState)
    {
        currentState = newState;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            TakeDamage(10); // 피해 값은 필요에 따라 조정 가능
        }
    }
}
