using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    CapsuleCollider2D CapsuleCollider2D;

    public Transform bossCollider;
    public GameObject Bossflip;  // Inspector에서 직접 할당

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Bossflip = GameObject.Find("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        float Bossf = Bossflip.GetComponent<BossController>().Bossflip;
        if (Bossf ==1)  // 보스가 좌우 반전되었을 때
               {
            CapsuleCollider2D.offset = new Vector2((CapsuleCollider2D.offset.x) * -1, CapsuleCollider2D.offset.y);
            //CapsuleCollider2D.enabled = false
            transform.localPosition = new Vector2(-Mathf.Abs(transform.localPosition.x), transform.localPosition.y);


        }
        else if(Bossf ==-1){
            CapsuleCollider2D.offset = new Vector2((CapsuleCollider2D.offset.x) * 1, CapsuleCollider2D.offset.y);
            transform.localPosition = new Vector2(Mathf.Abs(transform.localPosition.x), transform.localPosition.y);

            //CapsuleCollider2D.enabled = true;

        }
    }
}