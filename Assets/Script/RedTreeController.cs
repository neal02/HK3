using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTreeController : MonoBehaviour
{
    PolygonCollider2D poly2D;

    public int MaxTreeHp;

    void Start()
    {
        poly2D = GetComponent<PolygonCollider2D>();
        poly2D.enabled = false;
    }

    void Update()
    {
        if(BloodTree.clearSeal <= -90)
        {
            poly2D .enabled = true;
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            
        }
    }
}
