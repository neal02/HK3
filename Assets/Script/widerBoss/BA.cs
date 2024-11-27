using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA : MonoBehaviour
{
    CapsuleCollider2D CapsuleCollider2D;
    BoxCollider2D BoxCollider2D;
    public Transform bossCollider;
    public GameObject Bossflip;  // Inspector���� ���� �Ҵ�
    private Animator bossAnimator;

    private bool isPlayingAttackSound = false;  // ���� �Ҹ��� ��� ������ üũ

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        BoxCollider2D = GetComponent<BoxCollider2D>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        bossAnimator = Bossflip.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float bossFlipValue = Bossflip.GetComponent<BC>().Bossflip;

        float Bossf = Bossflip.GetComponent<BC>().Bossflip;
        if (Bossf == 1)  // ������ �¿� �����Ǿ��� ��
        {
            BoxCollider2D.offset = new Vector2(0.29f, 0);
            CapsuleCollider2D.offset = new Vector2((CapsuleCollider2D.offset.x) * -1, CapsuleCollider2D.offset.y);
            transform.localPosition = new Vector2(-Mathf.Abs(transform.localPosition.x), transform.localPosition.y);
        }
        else if (Bossf == -1)
        {
            BoxCollider2D.offset = new Vector2(-0.29f, 0);
            CapsuleCollider2D.offset = new Vector2((CapsuleCollider2D.offset.x) * 1, CapsuleCollider2D.offset.y);
            transform.localPosition = new Vector2(Mathf.Abs(transform.localPosition.x), transform.localPosition.y);
        }

        bool isAttacking = bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"); // "Attack"�� �ִϸ��̼� �̸�

        if (isAttacking)
        {
            if (!isPlayingAttackSound)  // �Ҹ��� ������� �ʾ��� ��쿡�� ���
            {
                isPlayingAttackSound = true;
                AudioManager.instance.StopSfx(AudioManager.Sfx.Boss_Attack);  // ���� ���� ���߱�
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Boss_Attack);  // ���� ���� ���� �ٷ� ���
                StartCoroutine(EnableBoxColliderWithDelay(0.5f)); // 0.5�� ������ �� BoxCollider Ȱ��ȭ
            }
        }
        else
        {
            StopAllCoroutines(); // ������ �ߴܵǸ� �ڷ�ƾ ����
            BoxCollider2D.enabled = false;
            isPlayingAttackSound = false;  // �Ҹ� ��� ����
        }
    }

    private void PlayAttackSound()
    {
        if (AudioManager.instance != null)
        {
            // ���� ���尡 ������ �ʾƵ� ���� ���۵ǰ� ��
            AudioManager.instance.StopSfx(AudioManager.Sfx.Boss_Attack);  // ���� ���� �ߴ�
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Boss_Attack);  // ���� ���� ���
        }
    }

    private IEnumerator EnableBoxColliderWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        BoxCollider2D.enabled = true;
    }
}
