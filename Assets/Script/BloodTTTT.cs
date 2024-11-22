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
        if (BloodTree.isSeal && !isClearing && BloodTree.clearSeal % 10 == 0  && BloodTree.clearSeal >= -40)
        {
            StartCoroutine(Clearing());
        }

        if (BloodTree.clearSeal == -50 && !isComplete)
        {
            //Debug.Log("-100");
            animator.SetBool("isPerfect", true);
            isComplete = true; 
        }
    }

    IEnumerator Clearing()
    {
        isClearing = true;
        yield return new WaitForEndOfFrame();
        if (BloodTree.clearSeal != -50)
            animator.SetBool("isClear", true);
        yield return new WaitForSeconds(1.33f);
        animator.SetBool("isClear", false);
        

        isClearing = false;

        if (BloodTree.clearSeal < -50)
        {
            BloodTree.isSeal = false; 
        }
    }
}

