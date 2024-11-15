using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class fireEnemy : MonoBehaviour
{
    GameObject player;

    public float XfireSpeed = 0.01f;
    public float YfireSpeed = 0.005f;

    void Start()
    {
        Application.targetFrameRate = 60;
        player = GameObject.Find("player");
        Destroy(gameObject, 9);
    }

    void Update()
    {
        /*playerX = player.transform.position.x;
        playerY = player.transform.position.y + 0.5f;
        disRate = Mathf.Abs((transform.position.x - playerX) / (transform.position.y - playerY));
        if (transform.position.x - playerX > 0)  //플레이어를 따라가는 함수
        {
            transform.position += new Vector3((XfireSpeed * -1), 0, 0);
        }
        if (transform.position.x - playerX < 0)
        {
            transform.position += new Vector3(XfireSpeed , 0, 0);
        }
        if(transform.position.y - playerY > 0)
        {
            transform.position += new Vector3(0, (XfireSpeed * -1), 0);
        }
        if((transform.position.y -playerY < 0))
        {
            transform.position += new Vector3(0, XfireSpeed, 0);
        }*/
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, XfireSpeed * Time.deltaTime);
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
