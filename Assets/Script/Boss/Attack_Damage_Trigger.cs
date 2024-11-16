using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Damage_Trigger : MonoBehaviour
{
    public bool isAttacking;

    void Start()
    {
        gameObject.SetActive(false);
        isAttacking = false;
    }

    public IEnumerator GetDamage()
    {
        if(!isAttacking)
        {
            yield break;
        }

        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        isAttacking = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.CompareTag("Player"))
        {
            Debug.Log("기본공격 맞음");
            isAttacking = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }
}
