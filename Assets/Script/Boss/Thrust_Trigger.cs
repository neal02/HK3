using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust_Trigger : MonoBehaviour
{
    public Boss boss;
    public float stayTime;
    public bool isInTrigger;
    public GameObject player;

    void Start()
    {
        stayTime = 0f; // 초기화
        isInTrigger = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            stayTime = 0f; // 플레이어가 들어오면 타이머 초기화
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            stayTime += Time.deltaTime; // 플레이어가 트리거에 머무는 동안 시간 증가

            if (stayTime >= 2.0f)
            {
                Debug.Log("2초 이상 머무름");
                isInTrigger = true;
                stayTime = 0f; // 2초가 지나면 타이머 초기화
            }

            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            stayTime = 0f; // 플레이어가 나가면 타이머 초기화
            isInTrigger = false;
        }
    }
}
