using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGenerator : MonoBehaviour
{
    public GameObject flowerPrefab;

    private float randposX = 0;
    private float randposY = 0;

    Quaternion rotation = Quaternion.identity;
    Vector3 pos;

    void Start()
    {
        Generator();
        Invoke("Generate",6);
    }

    void Update()
    {
        
    }

    void Generator()
    {
        randposX = Random.Range(-6.0f, 6.0f);
        randposY = Random.Range(-3.5f, -2.5f);
        pos = new Vector3(randposX, randposY, transform.position.z);
    }

    void Generate()
    {
        
        Instantiate(flowerPrefab, pos, rotation);
        Generator();
        Invoke("Generate", 8);
    }
}
