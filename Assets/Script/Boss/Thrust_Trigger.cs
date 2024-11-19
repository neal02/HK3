using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust_Trigger : MonoBehaviour
{
    public Boss boss;
    public float stayTime;
    public bool isInTrigger;
    public GameObject player;
    public float thrustDuration = 2f;
    bool isPlayerInTrigger = false;
    Boss bossScript;

    void Start()
    {
        stayTime = -1f; // 초기화
        isInTrigger = false;
        bossScript = boss.GetComponent<Boss>();
    }

    void Update()
    {
        if(isPlayerInTrigger && !bossScript.isThrusting && Time.time >= stayTime + thrustDuration)
        {
            isInTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            stayTime = Time.time;
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            stayTime = -1f;
            isInTrigger = false;
        }
    }
}
