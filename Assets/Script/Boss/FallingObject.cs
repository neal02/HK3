using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float fallingSpeed = -1.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if(transform.position.y <= -9.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("맞음");
            Destroy(gameObject);
        }
    }
}
