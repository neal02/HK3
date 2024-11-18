using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodTTTT : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    GameObject Lefttree;

    public int test;
    private bool isClearing;
    private bool isComplete; 

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Lefttree = GameObject.Find("LeftTree");
        isComplete = false;
    }

    void Update()
    {
        test = BloodTree.clearSeal;
        if (BloodTree.isSeal && !isClearing && BloodTree.clearSeal % 10 == 0 && BloodTree.clearSeal <= 0 && BloodTree.clearSeal >= -90)
        {
            StartCoroutine(Clearing());
        }

        if (BloodTree.clearSeal == -90 && !isComplete)
        {
            //Debug.Log("-100");
            animator.SetBool("isPerfect", true);
            isComplete = true; 
        }
    }

    IEnumerator Clearing()
    {
        isClearing = true;
        if (BloodTree.clearSeal != -90)
            animator.SetBool("isClear", true);
        //Debug.Log("안녕하세요");

        yield return new WaitForSeconds(1.33f);
        animator.SetBool("isClear", false);
        //Debug.Log("세요");
        yield return new WaitForEndOfFrame();

        isClearing = false;

        if (BloodTree.clearSeal <= -90)
        {
            BloodTree.isSeal = false; 
        }
    }
}

