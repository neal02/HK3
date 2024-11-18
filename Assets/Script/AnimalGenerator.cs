using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalGenerator : MonoBehaviour
{
    public GameObject ElkPrefab;
    public GameObject DeerPrefab;
    public GameObject RatPrefab;
    public GameObject FoxPrefab;

    private float randposX = 0;
    private float randposY = 0;
    private int randani = 0;
    private bool isGen = true;

    private GameObject currentPrefab;

    Quaternion rotation = Quaternion.identity;
    Vector3 pos;

    void Start()
    {
        //Invoke("Generate", 3);
    }

    private void FixedUpdate()
    {
        if(currentPrefab == null && isGen)
        {
            isGen = false;
            Invoke("Generate", 3);
        }
    }

    void Generator()
    {
        randposX = Random.Range(-6.0f, 6.0f);
        randposY = Random.Range(-3.5f, -2.5f);
        pos = new Vector3(randposX, randposY, transform.position.z);
    }

    void Generate()
    {
        Generator();
        randani = Random.Range(0, 4);
        if(randani == 0)
        {
            currentPrefab = Instantiate(ElkPrefab, pos, rotation);
        } else if(randani == 1)
        {
            currentPrefab = Instantiate(DeerPrefab, pos, rotation);
        } else if(randani == 2)
        {
            currentPrefab = Instantiate(RatPrefab, pos, rotation);
        } else if(randani == 3)
        {
            currentPrefab = Instantiate(FoxPrefab, pos, rotation);
        }
        isGen = true;
    }

    IEnumerator CheckDesroyed()
    {
        while(currentPrefab != null)
        {
            yield return null;
        }
        Invoke("Generate", 6);
    }
}
