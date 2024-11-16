using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack_Trigger : MonoBehaviour
{
    public Boss boss;
    public bool isDetecting;
    public GameObject attackDamageTrigger;
    public Attack_Damage_Trigger attackDamageTriggerScript;
    void Start()
    {
        isDetecting = false;
        attackDamageTriggerScript = attackDamageTrigger.GetComponent<Attack_Damage_Trigger>();
    }

    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.CompareTag("Player") && !isDetecting && !attackDamageTriggerScript.isAttacking)
        {
            isDetecting = true;
            Debug.Log("우선 감지됨");
            attackDamageTriggerScript.isAttacking = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.CompareTag("Player"))
        {
            isDetecting = true;
            attackDamageTriggerScript.isAttacking = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.CompareTag("Player") && isDetecting && attackDamageTriggerScript.isAttacking)
        {
            isDetecting = false;
            attackDamageTriggerScript.isAttacking = false;
        }
    }
}
