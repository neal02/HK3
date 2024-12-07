using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        if(transform.position.x <= 24 && transform.position.x >= 5)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 8, -10);
        }
        
    }
}
