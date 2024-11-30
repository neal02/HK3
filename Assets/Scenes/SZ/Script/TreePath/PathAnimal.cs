using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAnimal : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid2D;
    Animator animator;

    public float speed = 0;
    public float Limit = 0;

    private float invisible = 0;
    private int randdir;
    private bool isDeath = true;
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Think();
        spriteRenderer.color = new Color(spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a, invisible);
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        invisible += 0.05f;
        spriteRenderer.color = new Color(spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a, invisible);
        rigid2D.velocity = new Vector3(speed * randdir, 0, 0);
        if (rigid2D.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void Think()
    {
        randdir = Random.Range(0, 2);
        if (randdir == 0)
        {
            randdir = -1;
        }
        else
        {
            randdir = 1;
        }
        Invoke("Think", 5);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack") && isDeath)
        {
            isDeath = false;
            StartCoroutine(Death());
        }
        if (collision.CompareTag("Wall"))
        {
            Debug.Log("º®");
            randdir *= -1;
        }
    }

    IEnumerator Death()
    {
        PortalPath.PathCondition += 10;
        animator.SetBool("isDeath", true);
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}