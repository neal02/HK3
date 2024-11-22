using System.Collections;
using UnityEngine;

public class SpiderGenerator : MonoBehaviour
{
    public GameObject spiderPrefab;    // 재생성할 거미 프리팹
    public Vector2 spawnAreaMin;       // 스폰 영역 최소 좌표
    public Vector2 spawnAreaMax;       // 스폰 영역 최대 좌표
    public float respawnDelay = 1.0f;  // 재생성 딜레이

    private void Start()
    {
        SpawnSpider();
    }

    // 거미 생성 메서드
    public void SpawnSpider()
    {
        // 랜덤 스폰 위치 계산
        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject newSpider = Instantiate(spiderPrefab, randomPosition, Quaternion.identity);
        newSpider.GetComponent<CagedSpider>().OnDeath += HandleSpiderDeath;
    }

    // 거미가 죽었을 때 호출될 메서드
    private void HandleSpiderDeath()
    {
        StartCoroutine(RespawnSpiderAfterDelay());
    }

    // 지연 후 거미 재생성
    private IEnumerator RespawnSpiderAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnSpider();
    }
}
