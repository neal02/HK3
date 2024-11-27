using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public Image hpGauge; // ü�� ������ (������ �κ�)
    public Image bar;     // �� ü�� �� (ȸ�� �Ǵ� ���� ���)

    private float maxHealth = 100f; // �ִ� ü��
    private float currentHealth;    // ���� ü��

    void Start()
    {
        hpGauge = GameObject.Find("HpGauge").GetComponent<Image>();
        currentHealth = maxHealth; // ���� �� ü���� �ִ밪���� ����
        hpGauge.fillAmount = 1.0f;  // �ʱ� fillAmount�� 1�� ����
        UpdateHpGauge(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // ü�� ����
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // �ּ� ü�� ����
        UpdateHpGauge(currentHealth, maxHealth);
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount; // ü�� ȸ��
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // �ִ� ü�� ����
        UpdateHpGauge(currentHealth, maxHealth);
    }

    public void UpdateHpGauge(float currentHealth, float maxHealth)
    {
        float fillAmount = currentHealth / maxHealth;
        hpGauge.fillAmount = fillAmount;
    }
}
