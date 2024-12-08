using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance; // �̱��� �ν��Ͻ�
    public Image hpBar;
    public Image dashIcon;
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isDashing = false;

    void Awake()
    {
        if (Instance == null) {
            Instance = this; // ���� ��ü�� �̱��� �ν��Ͻ��� ����
            DontDestroyOnLoad(gameObject); // ��ü�� �ı����� �ʵ��� ����
        }
        else {
            // ���� ������ ��ü�� ���� ��ü�� ���� ������ ���� ������ ��ü�� �ʱ�ȭ���� �ʰ� ���
            SyncWithExistingInstance();
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHpBar();
        ResetDashIcon();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isDashing) {
            StartCoroutine(DashCooldown());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack")) {
            TakeDamage(10f);
        }
        if (collision.CompareTag("Healing")) {
            HealDamage();
        }
    }

    void HealDamage()
    {
        currentHealth = 100;
        UpdateHpBar();
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        if (hpBar != null) {
            hpBar.fillAmount = currentHealth / maxHealth;
        }
    }

    IEnumerator DashCooldown()
    {
        isDashing = true;
        float duration = 3f;
        float elapsedTime = 0f;

        if (dashIcon != null) {
            dashIcon.fillAmount = 0f;
        }

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            if (dashIcon != null) {
                dashIcon.fillAmount = elapsedTime / duration;
            }
            yield return null;
        }

        isDashing = false;
    }

    void ResetDashIcon()
    {
        if (dashIcon != null) {
            dashIcon.fillAmount = 1f;
        }
    }

    void SyncWithExistingInstance()
    {
        // ���� �ν��Ͻ��� ����ȭ: ���� ������ ��ü�� �����͸� ���� ��ü�� ����ϴ�.
        if (Instance != null) {
            currentHealth = Instance.currentHealth;
            maxHealth = Instance.maxHealth;
            isDashing = Instance.isDashing;
            hpBar = Instance.hpBar;
            dashIcon = Instance.dashIcon;

            // ���� ������ ������Ʈ�� ��Ȱ��ȭ�ϰų� ����
            Destroy(gameObject);
        }
    }
}
