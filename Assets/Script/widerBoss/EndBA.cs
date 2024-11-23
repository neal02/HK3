using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBA : MonoBehaviour
{
    CapsuleCollider2D CapsuleCollider2D;
    BoxCollider2D BoxCollider2D;
    public Transform bossCollider;
    public GameObject Bossflip;  // Inspector���� ���� �Ҵ�
    private Animator bossAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        BoxCollider2D = GetComponent<BoxCollider2D>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Bossflip = GameObject.Find("EndBoss");
        bossAnimator = Bossflip.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float bossFlipValue = Bossflip.GetComponent<EndBC>().Bossflip;

        float Bossf = Bossflip.GetComponent<EndBC>().Bossflip;
        if (Bossf ==1)  // ������ �¿� �����Ǿ��� ��
               {
            BoxCollider2D.offset = new Vector2(0.29f, 0);

            CapsuleCollider2D.offset = new Vector2((CapsuleCollider2D.offset.x) * -1, CapsuleCollider2D.offset.y);
            //CapsuleCollider2D.enabled = false
            transform.localPosition = new Vector2(-Mathf.Abs(transform.localPosition.x), transform.localPosition.y);


        }
        else if(Bossf ==-1){
            BoxCollider2D.offset = new Vector2(-0.29f, 0);
            CapsuleCollider2D.offset = new Vector2((CapsuleCollider2D.offset.x) * 1, CapsuleCollider2D.offset.y);
            transform.localPosition = new Vector2(Mathf.Abs(transform.localPosition.x), transform.localPosition.y);

            //CapsuleCollider2D.enabled = true;

        }
        bool isAttacking = bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"); // "BossAttackAnimation"�� �ִϸ��̼� �̸�
        if (isAttacking) {
            StartCoroutine(EnableBoxColliderWithDelay(0.5f)); // 1�� ������ �� BoxCollider Ȱ��ȭ
        }
        else {
            StopAllCoroutines(); // ������ �ߴܵǸ� �ڷ�ƾ ����
            BoxCollider2D.enabled = false;
        }

    }
    private IEnumerator EnableBoxColliderWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        BoxCollider2D.enabled = true;
    }
}