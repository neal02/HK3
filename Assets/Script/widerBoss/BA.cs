using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA : MonoBehaviour
{
    CapsuleCollider2D CapsuleCollider2D;
    BoxCollider2D BoxCollider2D;
    public Transform bossCollider;
    public GameObject Bossflip;  // Inspector에서 직접 할당
    private Animator bossAnimator;

    private bool isPlayingAttackSound = false;  // 공격 소리가 재생 중인지 체크

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
        if (Bossf == 1)  // 보스가 좌우 반전되었을 때
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

        bool isAttacking = bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"); // "Attack"은 애니메이션 이름

        if (isAttacking)
        {
            if (!isPlayingAttackSound)  // 소리가 재생되지 않았을 경우에만 재생
            {
                isPlayingAttackSound = true;
                AudioManager.instance.StopSfx(AudioManager.Sfx.Boss_Attack);  // 이전 사운드 멈추기
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Boss_Attack);  // 보스 공격 사운드 바로 재생
                StartCoroutine(EnableBoxColliderWithDelay(0.5f)); // 0.5초 딜레이 후 BoxCollider 활성화
            }
        }
        else
        {
            StopAllCoroutines(); // 공격이 중단되면 코루틴 중지
            BoxCollider2D.enabled = false;
            isPlayingAttackSound = false;  // 소리 재생 중지
        }
    }

    private void PlayAttackSound()
    {
        if (AudioManager.instance != null)
        {
            // 이전 사운드가 끝나지 않아도 새로 시작되게 함
            AudioManager.instance.StopSfx(AudioManager.Sfx.Boss_Attack);  // 이전 사운드 중단
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Boss_Attack);  // 새로 사운드 재생
        }
    }

    private IEnumerator EnableBoxColliderWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        BoxCollider2D.enabled = true;
    }
}
