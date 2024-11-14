using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireFlower : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;

    public GameObject firePrefab;
    public float fireGap = 3.0f;
    public float flowerSpeed = 0.01f;
    public int flowerHP = 2;


    private float deathcon = -0.5f;
    private float hitcon = -0.5f;
    private bool isdelay = true;
    private int randdir = 0;
    Quaternion rotation = Quaternion.identity;
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Invoke("Think", 5);
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
    }

    IEnumerator SpawnFire()
    {
        isdelay = false;
        Instantiate(firePrefab, pos, rotation);
        yield return new WaitForSeconds(fireGap);
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
    }
}
