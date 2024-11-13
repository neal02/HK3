using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireEnemy : MonoBehaviour
{
    GameObject player;

    public float XfireSpeed = 0.01f;
    public float YfireSpeed = 0.005f;

    private float playerX;
    private float playerY;
    void Start()
    {
        Application.targetFrameRate = 60;
        player = GameObject.Find("player");
        Destroy(gameObject, 9);
    }

    void Update()
    {
        playerX = player.transform.position.x;
        playerY = player.transform.position.y + 0.5f;
        if(transform.position.x - playerX > 0)  //플레이어를 따라가는 함수
        {
            transform.position += new Vector3((XfireSpeed * -1), 0, 0);
        }
        if (transform.position.x - playerX < 0)
        {
            transform.position += new Vector3(XfireSpeed, 0, 0);
        }
        if(transform.position.y - playerY > 0)
        {
            transform.position += new Vector3(0, (YfireSpeed * -1), 0);
        }
        if((transform.position.y -playerY < 0))
        {
            transform.position += new Vector3(0, YfireSpeed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("뜨겁다");
            Destroy(gameObject);
        }
    }
}
