using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject LeftTree;

    void Start()
    {
        LeftTree.SetActive(false);
        
    }

    void Update()
    {
        if( PortalPath.PathCondition == 21)
        {
            LeftTree.SetActive(true);
        }
    }
}
