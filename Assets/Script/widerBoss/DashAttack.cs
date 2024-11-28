using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DashAttack : MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    [SerializeField] private Image dashIcon; // DashAttack ������ �̹���
    [SerializeField] private float cooldownTime = 1.0f; // ��Ÿ�� �ð� ����
    [SerializeField] private Image cooldownOverlay; // ��Ÿ�� ������ �����ִ� ���� UI

    private bool delay = true;

    void Start()
    {
        Application.targetFrameRate = 60;
        boxCollider2D = GetComponent<BoxCollider2D>();

        if (dashIcon == null || cooldownOverlay == null)
        {
            Debug.LogError("DashAttack ������ �Ǵ� ��Ÿ�� �������̰� �������� �ʾҽ��ϴ�.");
        }

        // ��Ÿ�� UI �ʱ�ȭ
        cooldownOverlay.fillAmount = 0f; // ���� �� ��Ÿ�� UI�� �������
    }

    void Update()
    {
        // DashAttack�� ���� ����
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            boxCollider2D.offset = new Vector2(Math.Abs(boxCollider2D.offset.x), boxCollider2D.offset.y);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            boxCollider2D.offset = new Vector2(Math.Abs(boxCollider2D.offset.x) * -1, boxCollider2D.offset.y);
        }

        // DashAttack �ߵ�
        if (Input.GetKeyDown(KeyCode.Z) && delay)
        {
            delay = false;
            boxCollider2D.enabled = true;
            boxCollider2D.size = new Vector2(0.59f, 0.09f);
            StartCoroutine(Delay());
        }
        else
        {
            boxCollider2D.enabled = false;
        }
    }

    IEnumerator Delay()
    {
        // ��Ÿ�� ����
        float elapsedTime = 0f;
        cooldownOverlay.fillAmount = 1f; // ��Ÿ�� UI�� �� ä�� ���·� ����
        dashIcon.color = new Color(0.5f, 0.5f, 0.5f, 1f); // �������� ��Ӱ� ����

        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / cooldownTime;

            // ��Ÿ�� UI�� ���� �پ��
            cooldownOverlay.fillAmount = 1f - progress;

            // ������ ������ ���� �����
            float brightness = Mathf.Lerp(0.5f, 1f, progress);
            dashIcon.color = new Color(brightness, brightness, brightness, 1f);

            yield return null;
        }

        // ��Ÿ�� ����
        delay = true;
        cooldownOverlay.fillAmount = 0f; // ��Ÿ�� UI �ʱ�ȭ
        dashIcon.color = new Color(1f, 1f, 1f, 1f); // ������ ������ ������ ��� ����
    }
}
