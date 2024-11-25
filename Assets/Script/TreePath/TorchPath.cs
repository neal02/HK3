using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPath : MonoBehaviour
{
    Animator animator;

    public int TorchCondition = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(PortalPath.PathCondition == TorchCondition)
        {
            animator.SetBool("isTorch", true);
        }
    }
}