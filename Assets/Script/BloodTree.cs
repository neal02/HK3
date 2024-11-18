using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodTree : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;

    public static bool isSeal;
    public static int clearSeal;
    public int test;

    private bool isClearing;
    private bool isPerfectCalled; 

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        clearSeal = -80;
        isPerfectCalled = false;
    }

    void Update()
    {
        test = clearSeal;

        if (isSeal && !isClearing && clearSeal % 10 == 0 && clearSeal >= -90) 
        {
            StartCoroutine(Clearing());
        }

        if (clearSeal == 0 && !isPerfectCalled)
        {
            //Debug.Log("왼쪽 끝");
            animator.SetBool("isPerfect", true);
            isPerfectCalled = true; 
        }
    }

    IEnumerator Clearing()
    {
        isClearing = true;

        clearSeal -= 10;
        //Debug.Log(clearSeal);

        animator.SetBool("isClear", true);
        isSeal = false; // isSeal 초기화
        yield return new WaitForSeconds(1.33f);

        animator.SetBool("isClear", false);
        //Debug.Log("qq");
        isClearing = false;

        if (clearSeal < -90)
        {
            clearSeal = -90;
            isSeal = false; 
        }
    }
}

