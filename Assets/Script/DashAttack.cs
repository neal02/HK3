using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    public GameObject boss;
    Boss bossScript;

    private bool delay = true;

    void Start()
    {
        Application.targetFrameRate = 60;
        boxCollider2D = GetComponent<BoxCollider2D>();
        bossScript = boss.GetComponent<Boss>();
    }


    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            boxCollider2D.offset = new Vector2(Math.Abs(boxCollider2D.offset.x) , boxCollider2D.offset.y);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            boxCollider2D.offset = new Vector2(Math.Abs(boxCollider2D.offset.x) * -1, boxCollider2D.offset.y);
        }
        if (Input.GetKeyDown(KeyCode.Z) && delay)
        {
            delay = false;
            boxCollider2D.enabled = true;
            boxCollider2D.size = new Vector2(0.59f, 0.09f);
            StartCoroutine(Delay());
        }
        else
        {
            boxCollider2D.enabled = false;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        delay = true;
    }

    IEnumerator AttackedAnimation()
    {
        Debug.Log("되나");
        bossScript.anim.SetTrigger("isAttacked");
        yield return new WaitForSeconds(0.1f);
        bossScript.anim.SetTrigger("attackedReturn");
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Boss"))
        {
            Debug.Log("보스 때림");
            bossScript.hp -= 20;
            StartCoroutine(AttackedAnimation());
            Debug.Log("현재 체력: " + bossScript.hp);
        }
    }
}
