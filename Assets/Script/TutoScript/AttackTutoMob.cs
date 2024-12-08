using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTutoMob : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid2D;
    SpriteRenderer spriteRenderer;
    AudioSource[] audioSource;

    public BoxCollider2D StaightAttack;
    public GameObject StaightAttackObject;
    public GameObject player;

    public int MaxHp = 2;
    public float MaxSpeedX = 1;

    private bool AttackMode = false;
    public bool AttackCool = true;
    private bool isMobDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponents<AudioSource>();
        StaightAttackObject.SetActive(false);
    }

    void Update()
    {
        if (MaxHp <= 0 && !isMobDead)
        {
            AttackMode = false;
            animator.SetBool("isDeath", true);
            rigid2D.velocity = Vector3.zero;
            audioSource[0].Play();
            isMobDead = true;
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
                StaightAttack.offset = new Vector2(-0.27f, StaightAttack.offset.y);
                if (transform.position.x - player.transform.position.x < 3 && transform.position.x - player.transform.position.x > 1 && AttackCool)
                {
                    StartCoroutine(AttackDelay());
                }
            } 
            else if (transform.position.x - player.transform.position.x < -0.3)
            {
                rigid2D.velocity = new Vector3(MaxSpeedX, 0, 0);
                spriteRenderer.flipX = false;
                StaightAttack.offset = new Vector2(0.27f, StaightAttack.offset.y);
                if (transform.position.x - player.transform.position.x > -3 && transform.position.x - player.transform.position.x < -1 && AttackCool)
                {
                    StartCoroutine(AttackDelay());
                } 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack") && !StaightAttack.enabled)
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
        audioSource[1].Play();
        yield return new WaitForSeconds(0.67f);
        StartCoroutine(AttackCooled());
        animator.SetBool("isAttack", false);
    }

    void EnableStaightAttack()
    {
        StaightAttackObject.SetActive(true);
    }

    void DisableStaightAttack()
    {
        StaightAttackObject.SetActive(false);
    }

    IEnumerator AttackCooled()
    {
        AttackCool = false;
        yield return new WaitForSeconds(1f);
        AttackCool = true;
        yield return new WaitForSeconds(0.1f);
    }

}
