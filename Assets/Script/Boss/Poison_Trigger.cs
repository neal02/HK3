using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Trigger : MonoBehaviour
{
    public GameObject damageTrigger;

    IEnumerator ActivatePoisonDamageTrigger()
    {
        damageTrigger.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        damageTrigger.SetActive(false);
    }
}
