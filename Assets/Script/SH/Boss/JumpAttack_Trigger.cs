using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack_Trigger : MonoBehaviour
{
    public Boss boss;

    void Start()
    {

    }

    void Update()
    {
        transform.position = new Vector3(boss.transform.position.x, -6.5f, 0);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("점프 공격 맞음");
        }
    }
}
