using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class AlarmGenerator : MonoBehaviour
{
    public GameObject BranchAttack;

    private int randPos = 0;
    //private bool isOnce = true;
    private bool isDelay = true;

    Vector3[] pos;
    Quaternion[] rotation;

    void Start()
    {
        pos = new Vector3[5];
        rotation = new Quaternion[5];
        SpecPos();
        SpecRot();

    }

    void Update()
    {
        if (BloodTree.clearSeal <= -50 && isDelay)
        {
            StartCoroutine(Delay());
            SpawnBranch();
            
        }
    }

    private void FixedUpdate()
    {
        
    }

    void SpecPos()
    {
        pos[0] = new Vector3(8.5f, 5, 0);
        pos[1] = new Vector3(11.5f, -3, 0);
        pos[2] = new Vector3(-7f, 5.5f, 0);
        pos[3] = new Vector3(-10.5f, 2.5f, 0);
        pos[4] = new Vector3(-8.5f, 5, 0);
    }

    void SpecRot()
    {
        rotation[0] = Quaternion.Euler(0, 0, 120);
        rotation[1] = Quaternion.Euler(0, 0, 80);
        rotation[2] = Quaternion.Euler(0, 0, 210);
        rotation[3] = Quaternion.Euler(0, 0, 250);
        rotation[4] = Quaternion.Euler(0, 0, 240);
    }

    void SpawnBranch()
    {
        randPos = Random.Range(0, 5);
        Debug.Log(randPos);
        Instantiate(BranchAttack, pos[randPos], rotation[randPos]);
    }

    IEnumerator Delay()
    {
        isDelay = false;
        yield return new WaitForSeconds(4f);
        isDelay = true;
    }
}
