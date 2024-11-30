using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrachAttack : MonoBehaviour
{
    BoxCollider2D box2D;
    Animator animator;

    private bool isAppear = false;
    private bool isDisappear = false;
    void Start()
    {
        box2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        box2D.offset = new Vector2(0, 0);
        StartCoroutine(AlarmDelay());
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (box2D.offset.y < 0.6f && isAppear)
        {
            box2D.offset += new Vector2(0, 0.03f);
        }
        if (box2D.size.y < 1.3f && isAppear)
        {
            box2D.size += new Vector2(0, 0.06f);
        }
        if(!isAppear && isDisappear)
        {
            box2D.offset -= new Vector2(0, 0.03f);
            box2D.size -= new Vector2(0, 0.06f);
        }
    }

    IEnumerator AlarmDelay()
    {
        
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("isAlarm", true);
        StartCoroutine(AppearDelay());
        yield return new WaitForEndOfFrame();
    }

    IEnumerator AppearDelay()
    {
        isAppear = true;
        yield return new WaitForSeconds(0.7f);
        animator.SetBool("isAppear", true);
        StartCoroutine(DisAppearDelay());
        yield return new WaitForEndOfFrame();
    }

    IEnumerator DisAppearDelay()
    {
        isDisappear = true;
        yield return new WaitForSeconds(1.5f);
        isAppear = false;
        animator.SetBool("isDisAppear", true);
        yield return new WaitForSeconds(0.67f);
        Destroy(gameObject);
    }
}
