using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireFlower : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;

    public GameObject firePrefab;
    public float fireGap = 5.0f;
    public float flowerSpeed = 0.01f;
    public int flowerHP = 2;

    private float firecon = -0.5f;
    private float deathcon = -0.5f;
    private float hitcon = -0.5f;
    private bool isdelay = true;
    private int randdir = 0;
    private float invisible;
    Quaternion rotation = Quaternion.identity;
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Invoke("Think", 2);
        spriteRenderer.color = new Color(213f / 255f, 73f / 255f, 74f / 255f, invisible);
    }

    void Update()
    {
        transform.position += new Vector3(randdir * flowerSpeed,0,0);
        pos = transform.position;
        if (isdelay)
        {
            StartCoroutine(SpawnFire());
        }
        if(flowerHP <= 0)
        {
         
            animator.SetBool("isDeath2", true);
            animator.SetFloat("isDeath", deathcon);
            if (animator.GetFloat("isDeath") > 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        invisible += 0.05f;
        spriteRenderer.color = new Color(213f / 255f, 73f / 255f, 74f / 255f, invisible);
        if (animator.GetFloat("isHit") < 1)
        {
            hitcon += 0.03f;
            animator.SetFloat("isHit", hitcon);
        }
        if (animator.GetFloat("isDeath") < 1)
        {
            deathcon += 0.03f;
            animator.SetFloat("isDeath", deathcon);
        }
        if(animator.GetFloat("isFire") < 1)
        {
            firecon += 0.03f;
            animator.SetFloat("isFire", firecon);
        }
    }

    IEnumerator SpawnFire()
    {
        isdelay = false;
        firecon = -0.5f;
        animator.SetFloat("isFire", firecon);
        yield return new WaitForSeconds(fireGap);
        Instantiate(firePrefab, pos, rotation);
        
        isdelay = true;
    }


    void Think()
    {
        randdir = Random.Range(-1, 2);
        if(randdir < 0)
        {
            spriteRenderer.flipX = true;
        } else if(randdir > 0)
        {
            spriteRenderer.flipX = false;
        }
        Invoke("Think", 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            hitcon = -0.8f;
            animator.SetFloat("isHit", hitcon);
            flowerHP--;
            if(flowerHP <= 0)
            {
                deathcon = -0.5f;
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("º®");
            randdir *= -1;
        }
    }
}
