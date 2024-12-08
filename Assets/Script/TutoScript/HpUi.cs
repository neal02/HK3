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

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHpBar();
        ResetDashIcon();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 로드되었을 때 호출될 이벤트 등록
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 새로운 씬에서 플레이어의 위치를 초기화
        SetPlayerPosition(scene.name);
        AssignCameraToPlayer();
    }

    void AssignCameraToPlayer()
    {
        // 씬에서 Cinemachine Virtual Camera 찾기
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null) {
            // 버츄얼 카메라의 Follow 타겟을 현재 플레이어로 설정
            virtualCamera.Follow = this.transform;
        }
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

    void SetPlayerPosition(string sceneName)
    {
        // 씬 이름에 따라 초기 위치 설정
        switch (sceneName) {
            case "TutoScene":
                transform.position = new Vector3(-26, 0, 0); // Level1의 시작 위치
                break;
            case "Main":
                transform.position = new Vector3(-40, 0, 0); // Level2의 시작 위치
                break;
            case "PathTreeScene":
                transform.position = new Vector3(-2, 21, 0); // BossRoom의 시작 위치
                break;
            case "TreeScenes":
                transform.position = new Vector3(23, 0); // BossRoom의 시작 위치
                break;
            case "GateScene":
                transform.position = new Vector3(-7, -5, 0); // BossRoom의 시작 위치
                break;
            case "BattleScene":
                transform.position = new Vector3(19, -6, 0); // BossRoom의 시작 위치
                break;
            case "EndingScene":
                transform.position = new Vector3(4, -8, 0); // BossRoom의 시작 위치
                break;
            default:
                transform.position = new Vector3(0, 0, 0); // 기본 위치
                break;
        }
    }
}
