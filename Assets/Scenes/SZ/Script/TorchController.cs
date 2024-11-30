using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public GameObject[] torch;

    private Animator[] animator;

    void Start()
    {
        animator = new Animator[10];
        for(int i=0; i<torch.Length; i++)
        {
            animator[i] = torch[i].GetComponent<Animator>();
        }
    }

    void Update()
    {
        if(BloodTree.clearSeal == 40)
        {
            animator[0].SetBool("isTorch", true);
        }
        else if(BloodTree.clearSeal == 30)
        {
            animator[1].SetBool("isTorch", true);
        }
        else if (BloodTree.clearSeal == 20)
        {
            animator[2].SetBool("isTorch", true);
        }
        else if (BloodTree.clearSeal == 10)
        {
            animator[3].SetBool("isTorch", true);
        }
        else if (BloodTree.clearSeal == 0)
        {
            animator[4].SetBool("isTorch", true);
        }
        else if (BloodTree.clearSeal == -10)
        {
            animator[5].SetBool("isTorch", true);
        }
        else if (BloodTree.clearSeal == -20)
        {
            animator[6].SetBool("isTorch", true);
        }
        else if (BloodTree.clearSeal == -30)
        {
            animator[7].SetBool("isTorch", true);
        }
        else if (BloodTree.clearSeal == -40)
        {
            animator[8].SetBool("isTorch", true);
        }
        else if (BloodTree.clearSeal == -50)
        {
            animator[9].SetBool("isTorch", true);
        }
    }
}
