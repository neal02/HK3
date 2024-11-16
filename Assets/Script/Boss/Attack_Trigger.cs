using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack_Trigger : MonoBehaviour
{
    public GameObject boss;
    private Boss bossObject;
    private Rigidbody2D rigid;
    private Vector3 bossPosition;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        bossObject = boss.GetComponent<Boss>();
    }

    void FixedUpdate()
    {
        bossPosition = new Vector3(boss.transform.position.x, boss.transform.position.y, boss.transform.position.z);
        rigid.MovePosition(bossPosition);
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.CompareTag("Player"))
        {
            Debug.Log("플레이어 공격범위 들어옴");
            bossObject.isAttacking = true;
            bossObject.anim.SetBool("isAttacking", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.CompareTag("Player"))
        {
            Debug.Log("플레이어 공격범위 나감");
            bossObject.isAttacking = false;
            bossObject.anim.SetBool("isAttacking", false);
        }
    }*/
}
