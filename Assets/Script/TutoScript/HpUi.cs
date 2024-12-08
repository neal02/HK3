using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Image hpBar;
    public Image dashIcon;
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isDashing = false;
    private bool isPlayerDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHpBar();
        ResetDashIcon();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // ���� �ε�Ǿ��� �� ȣ��� �̺�Ʈ ���
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ����
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���ο� ������ �÷��̾��� ��ġ�� �ʱ�ȭ
        SetPlayerPosition(scene.name);
        AssignCameraToPlayer();
    }

    void AssignCameraToPlayer()
    {
        // ������ Cinemachine Virtual Camera ã��
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null) {
            // ����� ī�޶��� Follow Ÿ���� ���� �÷��̾�� ����
            virtualCamera.Follow = this.transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isDashing) {
            StartCoroutine(DashCooldown());
        }

        if (currentHealth <= 0 && !isPlayerDead) // if player is die
        {
            Debug.Log("사망");
            isPlayerDead = true;
            FindObjectOfType<SceneTransition>().EndBattleAndFadeOut("TutoScene");
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

    void SetPlayerPosition(string sceneName)
    {
        // �� �̸��� ���� �ʱ� ��ġ ����
        switch (sceneName) {
            case "TutoScene":
                transform.position = new Vector3(-26, 0, 0); // Level1�� ���� ��ġ
                break;
            case "Main":
                transform.position = new Vector3(-40, 0, 0); // Level2�� ���� ��ġ
                break;
            case "PathTreeScene":
                transform.position = new Vector3(-2, 21, 0); // BossRoom�� ���� ��ġ
                break;
            case "TreeScenes":
                transform.position = new Vector3(23, 0); // BossRoom�� ���� ��ġ
                break;
            case "GateScene":
                transform.position = new Vector3(-7, -5, 0); // BossRoom�� ���� ��ġ
                break;
            case "BattleScene":
                transform.position = new Vector3(19, -6, 0); // BossRoom�� ���� ��ġ
                break;
            case "EndingScene":
                transform.position = new Vector3(4, -8, 0); // BossRoom�� ���� ��ġ
                break;
            default:
                transform.position = new Vector3(0, 0, 0); // �⺻ ��ġ
                break;
        }
    }
}
