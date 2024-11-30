using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTree : MonoBehaviour
{
    BoxCollider2D box2D;
    Animator animator;

    public GameObject AnimalPrefab1;
    public GameObject AnimalPrefab2;
    public GameObject FlowerPrefab1;
    public GameObject FlowerPrefab2;

    private bool isFirst = true;
    private bool isFirsTouch = true;
    private bool isDes = true;
    private bool isDes2 = true;

    private GameObject[] ConditionPrefab;
    
    Quaternion rotation = Quaternion.identity;
    Vector3[] pos;

    void Start()
    {
        box2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        pos = new Vector3[4];
        ConditionPrefab = new GameObject[4];
        pos[0] = new Vector3(48, -3f, 0);
        pos[1] = new Vector3(53, -3.5f, 0);
        pos[2] = new Vector3(43, -2.5f, 0);
        pos[3] = new Vector3(38, -2.5f, 0);
    }

    void Update()
    {
        if (PortalPath.PathCondition == 1 && isFirst)
        {
            isFirst = false;
            ConditionPrefab[0] = Instantiate(AnimalPrefab1, pos[0], rotation);
            ConditionPrefab[1] = Instantiate(AnimalPrefab2, pos[1], rotation);
            StartCoroutine(SpawnDelay());
        }
        
        if (PortalPath.PathCondition == 11 && !isFirst && isDes)
        {
            isDes = false;
            StartCoroutine(Delay());
        } 
        else if (PortalPath.PathCondition == 21  && !isFirst && isDes2)
        {
            isDes2 = false;
            animator.SetBool("isPerfect", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((isFirsTouch))
        {
            isFirsTouch = false;
            PortalPath.PathCondition += 1;
        }
    }

    IEnumerator Delay()
    {
        animator.SetBool("isClear", true);
        yield return new WaitForSeconds(0.75f);
        animator.SetBool("isClear", false);
    }

    IEnumerator SpawnDelay()
    {
        ConditionPrefab[2] = Instantiate(FlowerPrefab1, pos[2], rotation);
        yield return new WaitForSeconds(1.5f);
        ConditionPrefab[3] = Instantiate(FlowerPrefab2, pos[3], rotation);
        yield return new WaitForSeconds(0.75f);
    }
}
