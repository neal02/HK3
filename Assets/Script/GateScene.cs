using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScene : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<SceneTransition>().EndBattleAndFadeOut("BattleScene");
        }

    }
}
