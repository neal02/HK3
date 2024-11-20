using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightFire : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rigid2D;
    public float XfireSpeed = 2f;

    int fireHp = 2;

    void Start()
    {
        Application.targetFrameRate = 60;
        rigid2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("player");

        Vector3 direction = (player.transform.position - transform.position).normalized;

        rigid2D.velocity = new Vector2(direction.x * XfireSpeed, direction.y * XfireSpeed);
    }

    void Update()
    {
        if(fireHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bottom"))
        {
            rigid2D.velocity = new Vector3(rigid2D.velocity.x, rigid2D.velocity.y  * -1);
            fireHp--;
        } else if (collision.gameObject.CompareTag("Wall"))
        {
            rigid2D.velocity = new Vector3(rigid2D.velocity.x * -1, rigid2D.velocity.y);
            fireHp--;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
