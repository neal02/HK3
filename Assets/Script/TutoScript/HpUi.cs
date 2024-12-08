using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance; // 싱글톤 인스턴스
    public Image hpBar;
    public Image dashIcon;
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isDashing = false;

    void Awake()
    {
        if (Instance == null) {
            Instance = this; // 현재 객체를 싱글톤 인스턴스로 설정
            DontDestroyOnLoad(gameObject); // 객체를 파괴하지 않도록 설정
        }
        else {
            // 새로 생성된 객체가 기존 객체와 같지 않으면 새로 생성된 객체를 초기화하지 않고 사용
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
        // 기존 인스턴스와 동기화: 새로 생성된 객체의 데이터를 원래 객체와 맞춥니다.
        if (Instance != null) {
            currentHealth = Instance.currentHealth;
            maxHealth = Instance.maxHealth;
            isDashing = Instance.isDashing;
            hpBar = Instance.hpBar;
            dashIcon = Instance.dashIcon;

            // 새로 생성된 오브젝트를 비활성화하거나 제거
            Destroy(gameObject);
        }
    }
}
