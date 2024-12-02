using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public GameObject portal;
    public GameObject boss;
    public Boss bossScript;

    bool portalSummoned = false;

    void Start()
    {
        bossScript = boss.GetComponent<Boss>();
        portal.SetActive(false);

    }

    void Update()
    {
        if (!bossScript.isAlive && !portalSummoned)
        {
            StartCoroutine(SummonPortal());
        }
    }

    IEnumerator SummonPortal()
    {
        portalSummoned = true;

        yield return new WaitForSeconds(3.0f);

        portal.SetActive(true);

    }
}
