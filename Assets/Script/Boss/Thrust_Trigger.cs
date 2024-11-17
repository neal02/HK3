using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust_Trigger : MonoBehaviour
{
    public Boss boss;
    public bool isDetecting;
    public bool isChecked;

    void Start()
    {
        isDetecting = false;
    }

    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.CompareTag("Player") && !isDetecting)
        {
            isDetecting = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.CompareTag("Player"))
        {
            isDetecting = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.CompareTag("Player") && isDetecting)
        {
            isDetecting = false;
        }
    }
}
