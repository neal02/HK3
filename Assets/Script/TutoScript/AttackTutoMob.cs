using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTutoMob : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid2D;
    SpriteRenderer spriteRenderer;

    public BoxCollider2D StaightAttack;
    public BoxCollider2D AirAttack;
    public GameObject player;

    public int MaxHp = 2;
    public float MaxSpeedX = 1;

    private bool AttackMode = false;
    public bool AttackCool = true;
    public bool AirAttackCool = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (MaxHp <= 0)
        {
            AttackMode = false;
            animator.SetBool("isDeath", true);
            rigid2D.velocity = Vector3.zero;
        }
        if(transform.position.x - player.transform.position.x < 10)
        {
            AttackMode = true;
            animator.SetBool("isOn", true);
        }
    }

    private void FixedUpdate()
    {
        if (AttackMode && MaxHp > 0)
        {
            if(transform.position.x - player.transform.position.x > 0.3)
            {
                rigid2D.velocity = new Vector3(MaxSpeedX * -1, 0, 0);
                spriteRenderer.flipX = true;
                if (transform.position.x - player.transform.position.x < 3 && transform.position.x - player.transform.position.x > 1 && AttackCool)
                {
                    StartCoroutine(AttackDelay());
                }
                else if (transform.position.x - player.transform.position.x < 3 && AirAttackCool)
                {
                    StartCoroutine(AirAttackDelay());
                }
            } 
            else if (transform.position.x - player.transform.position.x < -0.3)
            {
                rigid2D.velocity = new Vector3(MaxSpeedX, 0, 0);
                spriteRenderer.flipX = false;
                if (transform.position.x - player.transform.position.x > -3 && transform.position.x - player.transform.position.x < -1 && AttackCool)
                {
                    StartCoroutine(AttackDelay());
                } 
                else if (transform.position.x - player.transform.position.x > -1 && AirAttackCool)
                {
                    StartCoroutine(AirAttackDelay());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack") && this.CompareTag("Enemy"))
        {
            if (MaxHp > 0)
            {
                StartCoroutine(Delay());
            }
        }
    }

    IEnumerator Delay()
    {
        MaxHp--;
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(0.18f);
        animator.SetBool("isHit", false);
    }

    IEnumerator AttackDelay()
    {
        animator.SetBool("isAttack", true);
        StaightAttack.enabled = true;
        yield return new WaitForSeconds(0.67f);
        StaightAttack.enabled = false;
        StartCoroutine(AttackCooled());
        animator.SetBool("isAttack", false);
    }

    IEnumerator AirAttackDelay()
    {
        animator.SetBool("isAirAttack", true);
        AirAttack.enabled = true;
        yield return new WaitForSeconds(0.67f);
        AirAttack.enabled = false;
        StartCoroutine(AirAttackCooled());
        animator.SetBool("isAirAttack", false);
    }

    IEnumerator AttackCooled()
    {
        AttackCool = false;
        yield return new WaitForSeconds(3f);
        AttackCool = true;
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator AirAttackCooled()
    {
        AirAttackCool = false;
        yield return new WaitForSeconds(3f);
        AirAttackCool = true;
        yield return new WaitForSeconds(0.1f);
    }
}
