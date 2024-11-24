using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Damage_Trigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("독 맞음");
        }
    }
}
