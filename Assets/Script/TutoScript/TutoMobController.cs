using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoMobController : MonoBehaviour
{
    Animator animator;

    public int MaxHp = 2;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(MaxHp <= 0)
        {
            StartCoroutine(DeathDelay());
        }

    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            if(MaxHp > 0)
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
    IEnumerator DeathDelay()
    {
        animator.SetBool("isDeath", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isDeath", false);
    }
}
