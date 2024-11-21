using System.Collections;
using UnityEngine;

public class BlackHoleGenerator : MonoBehaviour
{
    public GameObject blackHolePrefab;  // ��Ȧ ������
    public float spawnInterval = 1f;    // ��Ȧ ���� ����
    public float minX = -5f;            // X�� �ּҰ�
    public float maxX = 5f;             // X�� �ִ밪
    public float spawnHeight = 10f;     // ��Ȧ ���� ����

    void Start()
    {
        // ��Ȧ ���� ����
        StartCoroutine(SpawnBlackHoles());
    }

    // ��Ȧ �����ϴ� �ڷ�ƾ
    private IEnumerator SpawnBlackHoles()
    {
        while (true) // ���� ����, ��� ����
        {
            // ���� �ð� �������� ��Ȧ ����
            SpawnBlackHole();
            yield return new WaitForSeconds(spawnInterval); // spawnInterval �ð� �������� ����
        }
    }

    // ��Ȧ ���� �޼ҵ�
    private void SpawnBlackHole()
    {
        // ��Ȧ �������� ������ ��ġ�� ����
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), spawnHeight, 0f);
        Instantiate(blackHolePrefab, spawnPosition, Quaternion.identity);
    }
}
