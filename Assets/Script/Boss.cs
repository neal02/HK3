using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    // 보스의 체력
    protected float health;
    
    // 페이즈 단계
    protected int phase;

    // 보스의 공격 속도 및 패턴 설정
    protected float attackSpeed;
    protected float movementSpeed;

    // 보스가 현재 공격 중인지 여부
    protected bool isAttacking;

    //보스가 플레이어를 감지했는지 여부
    protected bool isDetecting;

    //보스 리지드바디
    protected Rigidbody2D bossRigid;

    //충돌 관련 변수
    private Collider2D bossCollider;
    private Collider2D playerCollider;

    protected Transform playerTransform;
 
    //애니매이션 관련 변수
    protected Animator bossAnim;
    protected bool isWalking;
    // 초기화 메서드
    protected virtual void Start()
    {
        health = 100f; // 기본 체력
        phase = 1;     // 시작 페이즈
        attackSpeed = 1.0f;
        movementSpeed = 2.0f;
        bossCollider = GetComponent<Collider2D>();
        bossRigid = GetComponent<Rigidbody2D>();
        bossAnim = GetComponent<Animator>();

        isWalking = true;

        GameObject player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            playerTransform = player.transform;
            playerCollider = player.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(bossCollider, playerCollider, true);
        }
    }

    // 공격 행동을 정의할 추상 메서드
    public abstract void Attack();

    // 이동 행동을 정의할 추상 메서드
    public abstract void Move();

    // 페이즈 변경 로직을 정의할 추상 메서드
    public abstract void ChangePhase();

    // 보스가 피격되었을 때 호출되는 메서드
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            // 체력에 따라 페이즈 변경
            if (health < 50 && phase == 1)
            {
                phase = 2;
                ChangePhase();
            }
        }
    }

    // 보스가 사망할 때 실행되는 메서드
    protected virtual void Die()
    {
        // 보스 사망 처리 로직
        Debug.Log("Boss has been defeated!");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 플레이어와의 트리거 이벤트 처리
            Debug.Log("Boss triggered by player!");
            // 예: 플레이어에게 데미지 주기 등
            isDetecting = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            // 맵과의 충돌 처리
            Debug.Log("Boss collided with map element.");
            isWalking = true;
        }
    }
}
