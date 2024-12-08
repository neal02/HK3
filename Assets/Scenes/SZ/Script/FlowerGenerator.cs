using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGenerator : MonoBehaviour
{
    public GameObject flowerPrefab;

    private float randposX = 0;
    private float randposY = 0;

    private GameObject[] currentPrefab;
    private int currentIndex = 0;
    private bool isGen = true;

    Quaternion rotation = Quaternion.identity;
    Vector3 pos;

    void Start()
    {
        currentPrefab = new GameObject[3];
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        if (currentPrefab[0] == null && isGen)
        {
            isGen = false;
            currentIndex = 0;
            Invoke("Generate", 5);
        }
        else if (currentPrefab[1] == null && isGen)
        {
            isGen = false;
            currentIndex = 1;
            Invoke("Generate", 5);
        }
        else if (currentPrefab[2] == null && isGen)
        {
            isGen = false;
            currentIndex = 2;
            Invoke("Generate", 5);
        }

    }

    void Generator()
    {
        randposX = Random.Range(17.0f, 29.0f);
        randposY = Random.Range(-3.5f, -2.5f);
        pos = new Vector3(randposX, randposY, transform.position.z);
    }

    void Generate()
    {
        Generator();
        currentPrefab[currentIndex] = Instantiate(flowerPrefab, pos, rotation);
        currentIndex++;
        isGen = true;
    }
}
