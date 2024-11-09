using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss1 : Boss
{
    protected override void Start()
    {
        base.Start();

        health = 200f; // 기본 체력
        phase = 1;     // 시작 페이즈
        attackSpeed = 1.0f;
        movementSpeed = 3.0f;
    }

    public override void Attack()
    {
        
    }

    public override void Move()
    {

    }

    public override void ChangePhase()
    {

    }

    void FixedUpdate()
    {
        if(isDetecting)
        {
            if(playerTransform != null)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;

                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, movementSpeed / 50);
                //bossAnim.SetBool("isWalking", true);
            }
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            
        }
    }
}
