using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    BoxCollider2D boxCollider2D;


    private bool delay = true;

    void Start()
    {
        Application.targetFrameRate = 60;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            boxCollider2D.offset = new Vector2(Math.Abs(boxCollider2D.offset.x) * -1, boxCollider2D.offset.y);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            boxCollider2D.offset = new Vector2(Math.Abs(boxCollider2D.offset.x) * 1, boxCollider2D.offset.y);
        }
        if (Input.GetKeyDown(KeyCode.X) && delay)
        {
            delay = false;
            boxCollider2D.enabled = true;
            boxCollider2D.size = new Vector2(0.17f, 0.14f);
            StartCoroutine(Delay());
        } else
        {
            boxCollider2D.enabled = false;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        delay = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Boss"))
        {
            Debug.Log("보스 때림");
        }
    }
}
