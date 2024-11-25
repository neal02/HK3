using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust_Trigger : MonoBehaviour
{
    public bool isInTrigger = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }
}
