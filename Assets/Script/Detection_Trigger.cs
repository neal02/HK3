using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detection_Trigger : MonoBehaviour
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
        if (other.gameObject.tag.Equals("Attack"))
        {
            Debug.Log("공격");
            bossScript.TakeDamage(10);
            Debug.Log(bossScript.currentHp);
        }
    }
}
