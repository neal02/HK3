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
        Instantiate(flowerPrefab, pos, rotation);
    }

    void Update()
    {
        
    }

    void Generator()
    {
        randposX = Random.Range(-4.0f, -3.0f);
        pos = new Vector3(randposX, 0, transform.position.z);
    }
}
