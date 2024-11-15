using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack_Trigger : MonoBehaviour
{
    public GameObject boss;
    private Boss bossScript;
    private Rigidbody2D rigid;
    private Vector3 bossPosition;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        bossScript = boss.GetComponent<Boss>();
    }

    void FixedUpdate()
    {
        bossPosition = new Vector3(boss.transform.position.x + 1f, boss.transform.position.y + 2.0f, boss.transform.position.z);
        rigid.MovePosition(bossPosition);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && bossScript != null)
        {
            Debug.Log("플레이어 공격범위 들어옴");
            bossScript.isAttacking = true;
            bossScript.anim.SetBool("isAttacking", true);
            bossScript.Attack();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && bossScript != null)
        {
            Debug.Log("플레이어 공격범위 나감");
            bossScript.isAttacking = false;
            bossScript.anim.SetBool("isAttacking", false);
        }
    }
}
