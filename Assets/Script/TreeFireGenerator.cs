using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TreeFireGenerator : MonoBehaviour
{
    public GameObject animalGenerator;
    public GameObject flowerGenerator;
    public GameObject StraightFirePrefab;

    private bool isDelay = true;
    private bool isDDDD = true;
    private int index = 0;

    private bool RisDelay = true;
    private bool RisDDDD = true;
    private int Rindex = 0;

    Quaternion rotation = Quaternion.identity;
    Vector3[] Leftpos;
    Vector3[] Rightpos;

    void Start()
    {
        Leftpos = new Vector3[7];
        Rightpos = new Vector3[7];
        DecideLeftPos();
        DecideRightPos();
    }

    void Update()
    {
        if(BloodTree.clearSeal <= -90)
        {
            Destroy(animalGenerator);
            Destroy(flowerGenerator);
            
        } 
        if(BloodTree.clearSeal <= 0)
        {
            if (isDelay && isDDDD)
            {
                isDelay = false;
                Instantiate(StraightFirePrefab, Leftpos[index], rotation);
                StartCoroutine(SpawnDelay());
            }
        }
        if (index == 7)
        {
            StartCoroutine(CycleDelay());
            index = 0;
        }

        if (BloodTree.clearSeal <= -90)
        {
            if (RisDelay && RisDDDD)
            {
                RisDelay = false;
                Instantiate(StraightFirePrefab, Rightpos[Rindex], rotation);
                StartCoroutine(RSpawnDelay());
            }
        }
        if (Rindex == 7)
        {
            StartCoroutine(RCycleDelay());
            Rindex = 0;
        }
    }

    void DecideLeftPos()
    {
        Leftpos[0] = new Vector3(-7.1f, -1.5f, 0);
        Leftpos[1] = new Vector3(-7.1f, 0.9f, 0);
        Leftpos[2] = new Vector3(-6.3f, 0.7f, 0);
        Leftpos[3] = new Vector3(-3.5f, 0.6f, 0);
        Leftpos[4] = new Vector3(-6.7f, 2, 0);
        Leftpos[5] = new Vector3(-5, 2, 0);
        Leftpos[6] = new Vector3(-1.6f, 1.2f, 0);
    }

    void DecideRightPos()
    {
        Rightpos[0] = new Vector3(7.1f, -1.5f, 0);
        Rightpos[1] = new Vector3(7.1f, 0.9f, 0);
        Rightpos[2] = new Vector3(6.3f, 0.7f, 0);
        Rightpos[3] = new Vector3(3.5f, 0.6f, 0);
        Rightpos[4] = new Vector3(6.7f, 2, 0);
        Rightpos[5] = new Vector3(5, 2, 0);
        Rightpos[6] = new Vector3(1.6f, 1.2f, 0);
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1.5f);
        isDelay = false;
        index++;
        yield return new WaitForSeconds(0.5f);
        isDelay = true;
    }

    IEnumerator CycleDelay()
    {
        isDDDD = false;
        yield return new WaitForSeconds(6);
        isDDDD = true;
    }

    IEnumerator RSpawnDelay()
    {
        yield return new WaitForSeconds(1.5f);
        RisDelay = false;
        Rindex++;
        yield return new WaitForSeconds(0.5f);
        RisDelay = true;
    }

    IEnumerator RCycleDelay()
    {
        RisDDDD = false;
        yield return new WaitForSeconds(6);
        RisDDDD = true;
    }
}
