using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public GameObject boss;
    public Boss bossScript;

    void Start()
    {
        bossScript = boss.GetComponent<Boss>();
        bossScript.bossAudioSources[0].Play();
    }

    void Update()
    {
        if (bossScript.hp <= 0)
        {
            bossScript.bossAudioSources[0].Stop();
        }
    }
}
