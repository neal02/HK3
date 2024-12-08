using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using Cinemachine;

public class CagedSpider : MonoBehaviour
{
    private Animator animator;
    public string animationTriggerName = "Hit";  // �ִϸ��̼� Ʈ���� �̸�
    public Tilemap currentTilemap;  // ���� Ÿ�ϸ�
    public Tilemap DestroyTilemap;  // ���� Ÿ�ϸ�
    public Tilemap nextTilemap;     // ���� Ÿ�ϸ�
    public Tilemap nextTilemap2;     // ���� Ÿ�ϸ�
    public GameObject bossObject;   // ���� ��ü
    public GameObject bossObject2;   // ���� ��ü


    private void Start()
    {
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();
    }

    // Ʈ���� �浹 ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack")) {
            animator.SetTrigger(animationTriggerName);

            // �ִϸ��̼� ���� Ȯ�� �� �ڷ�ƾ���� ó��
            StartCoroutine(DestroyAfterDelay(0.6f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Ÿ�ϸ� ���� �۾�
        if (currentTilemap != null)
            currentTilemap.gameObject.SetActive(false);

        if (DestroyTilemap != null)
            DestroyTilemap.gameObject.SetActive(false);

        if (nextTilemap != null)
            nextTilemap.gameObject.SetActive(true);


        if (nextTilemap2 != null)
            nextTilemap2.gameObject.SetActive(true);

        // ī�޶� ���� �۾�
        AssignCameraToPlayer();

        // ���� �ð� �� ������Ʈ �ı�
        Destroy(gameObject);
    }

    private void AssignCameraToPlayer()
    {
        // Ȱ��ȭ�� Ÿ�ϸʿ��� ����� ī�޶� ã��
        Tilemap[] activeTilemaps = { nextTilemap, nextTilemap2 };

        foreach (var tilemap in activeTilemaps) {
            if (tilemap != null && tilemap.gameObject.activeSelf) {
                CinemachineVirtualCamera virtualCamera = tilemap.GetComponentInChildren<CinemachineVirtualCamera>();
                if (virtualCamera != null) {
                    GameObject player = GameObject.FindWithTag("Player"); // �÷��̾� ã��
                    if (player != null) {
                        virtualCamera.Follow = player.transform; // �÷��̾ Follow�� ����
                    }
                }
            }
        }
    }
    private void AssignCameraToBoss()
    {
        // ���� ��ü���� CinemachineVirtualCamera�� ã�� �÷��̾ ���󰡵��� ����
        CinemachineVirtualCamera virtualCamera = bossObject.GetComponentInChildren<CinemachineVirtualCamera>();
        if (virtualCamera != null) {
            GameObject player = GameObject.FindWithTag("Player"); // �÷��̾� ã��
            if (player != null) {
                virtualCamera.Follow = player.transform; // �÷��̾ Follow�� ����
            }
        }
    }
}