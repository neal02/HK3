using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class RedTreeController : MonoBehaviour
{
    PolygonCollider2D poly2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public GameObject Phase2;
    TreeFireGenerator treeFireGenerator;

    public int MaxTreeHp = 100;

    void Start()
    {
        poly2D = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        treeFireGenerator = Phase2.GetComponent<TreeFireGenerator>();

        poly2D.enabled = false;
    }

    void Update()
    {
        if(BloodTree.clearSeal <= -90)
        {
            poly2D .enabled = true;
        }    
        if(MaxTreeHp <= 0)
        {
            //Destroy(gameObject);
            poly2D.enabled = false;
            treeFireGenerator.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            MaxTreeHp -= 10;
            StartCoroutine(Delay());
        }
        if(MaxTreeHp < 0)
        {
            spriteRenderer.color = new Color(0, 0, 0);
        }
    }

    IEnumerator Delay()
    {
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(0.3f) ;
        animator.SetBool("isHit", false);
    }
}
