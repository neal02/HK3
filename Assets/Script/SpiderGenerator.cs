using System.Collections;
using UnityEngine;

public class SpiderGenerator : MonoBehaviour
{
    public GameObject spiderPrefab;    // ������� �Ź� ������
    public Vector2 spawnAreaMin;       // ���� ���� �ּ� ��ǥ
    public Vector2 spawnAreaMax;       // ���� ���� �ִ� ��ǥ
    public float respawnDelay = 1.0f;  // ����� ������

    private void Start()
    {
        SpawnSpider();
    }

    // �Ź� ���� �޼���
    public void SpawnSpider()
    {
        // ���� ���� ��ġ ���
        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject newSpider = Instantiate(spiderPrefab, randomPosition, Quaternion.identity);
        newSpider.GetComponent<CagedSpider>().OnDeath += HandleSpiderDeath;
    }

    // �Ź̰� �׾��� �� ȣ��� �޼���
    private void HandleSpiderDeath()
    {
        StartCoroutine(RespawnSpiderAfterDelay());
    }

    // ���� �� �Ź� �����
    private IEnumerator RespawnSpiderAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnSpider();
    }
}
