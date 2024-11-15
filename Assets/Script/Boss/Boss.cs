using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour //보스의 본체 스크립트, 본체 스크립트에선 보스의 에니메이션과 변수, 함수, 그리고 충돌 콜라이더 구현, 다른 기믹에 대한 구현은 자식 오브젝트에서 함
{

    private Rigidbody2D rigid;
    public Animator anim;
    public GameObject player;
    private Collider2D playerCollider, bossCollider;
    private SpriteRenderer sprite;

    public bool isDetecting;
    public bool isAttacking = false;

    public float moveSpeed;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("player");
        playerCollider = player.GetComponent<Collider2D>();
        bossCollider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();

        Physics2D.IgnoreCollision(bossCollider, playerCollider, true);

        moveSpeed = 1.0f;
        isDetecting = false;

    }

    void FixedUpdate()
    {
        if (player.transform.position.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else if (player.transform.position.x > transform.position.x)
        {
            sprite.flipX = false;
        }

        if(isDetecting)  //보스전이 시작되면 우선 플레이어 위치 탐색 시작
        { 
            if(!isAttacking)
            {
                Move();
            }
            else
            {
                Attack();
            }
        }
    }

    void Move()
    {
        anim.SetBool("isMoving", true);
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed / 50);
    }

    void Attack()
    {
        if(anim.GetBool("isAttacking"))
        {
            anim.SetBool("isMoving", false);
            StartCoroutine(EndAttack());
        }
    }
    
    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(1.85f);
        isAttacking = false;
        anim.SetBool("isAttacking", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {

    }
}
