using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public Image hpGauge; // 체력 게이지 (빨간색 부분)
    public Image bar;     // 빈 체력 바 (회색 또는 검정 배경)

    private float maxHealth = 100f; // 최대 체력
    private float currentHealth;    // 현재 체력

    void Start()
    {
        hpGauge = GameObject.Find("HpGauge").GetComponent<Image>();
        currentHealth = maxHealth; // 시작 시 체력을 최대값으로 설정
        hpGauge.fillAmount = 1.0f;  // 초기 fillAmount를 1로 설정
        UpdateHpGauge(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // 체력 감소
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 최소 체력 제한
        UpdateHpGauge(currentHealth, maxHealth);
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount; // 체력 회복
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 최대 체력 제한
        UpdateHpGauge(currentHealth, maxHealth);
    }

    public void UpdateHpGauge(float currentHealth, float maxHealth)
    {
        float fillAmount = currentHealth / maxHealth;
        hpGauge.fillAmount = fillAmount;
    }
}
