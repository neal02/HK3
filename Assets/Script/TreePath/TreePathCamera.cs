using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePathCamera : MonoBehaviour
{
    public GameObject player;

    private float MimCameraX = 0f;
    private float MaxCameraX = 15f;

    void Start()
    {
        
    }


    void Update()
    {
        if(player.transform.position.x <= 0)
        {
            gameObject.transform.position = new Vector3(MimCameraX, 0, -10);
        } 
        else if (player.transform.position.x >= 40)
        {
            gameObject.transform.position = new Vector3(MaxCameraX, 0, -10);
        } else
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, 0, -10);
        }
    }
}
