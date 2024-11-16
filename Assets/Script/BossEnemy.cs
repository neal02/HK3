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
            Destroy(gameObject, 1.1f);// 애니메이션이 끝난 후 오브젝트 파괴

        }
    }
}
