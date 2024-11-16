using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
        Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Attack"))
        {
            animator.SetTrigger("Hit");
        }
    }
}
