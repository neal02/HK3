using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Detecting_Trigger : MonoBehaviour
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && other.CompareTag("Player")) //플레이어의 무기에 반응하진 않음
        {
            bossObject.isDetecting = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && other.CompareTag("Player")) //플레이어의 무기에 반응하진 않음
        {
            bossObject.isDetecting = false;
        }
    }
}
