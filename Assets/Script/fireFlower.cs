using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireFlower : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public GameObject firePrefab;
    public float fireGap = 3.0f;
    public float flowerSpeed = 0.01f;

    private bool isdelay = true;
    private int randdir = 0;
    Quaternion rotation = Quaternion.identity;
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
}
