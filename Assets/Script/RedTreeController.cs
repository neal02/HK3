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
    public GameObject LastDoor;
    TreeFireGenerator treeFireGenerator;

    public int MaxTreeHp = 100;

    private float treeColor = 0;
    private bool isInvin = true;
    void Start()
    {
        poly2D = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        treeFireGenerator = Phase2.GetComponent<TreeFireGenerator>();
        LastDoor.SetActive(false);
        spriteRenderer.color = new Color(treeColor, treeColor, treeColor);
        poly2D.enabled = false;
        animator.enabled = false;
    }

    void Update()
    {
        if(BloodTree.clearSeal <= -50)
        {
            animator.enabled = true;
            poly2D .enabled = true;
        }    
    }

    private void FixedUpdate()
    {
        if (BloodTree.clearSeal <= -50 && MaxTreeHp > 0)
        {
            if (treeColor <= 1)
            {
                treeColor += 0.03f;
                spriteRenderer.color = new Color(treeColor, treeColor, treeColor);
            }
        }

        if(MaxTreeHp <= 0)
        {
            treeColor -= 0.01f;
            spriteRenderer.color = new Color(treeColor, treeColor, treeColor);
            poly2D.enabled = false;
            treeFireGenerator.enabled = false;
            LastDoor.SetActive(true);

            if(LastDoor.transform.position.y < -4f)
            {
                LastDoor.transform.position += new Vector3(0, 0.03f, 0);
            }
            poly2D.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack") && isInvin)
        {
            MaxTreeHp -= 10;
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        animator.SetBool("isHit", true);
        isInvin = false;
        yield return new WaitForSeconds(0.3f) ;
        animator.SetBool("isHit", false);
        isInvin = true;
    }
}
