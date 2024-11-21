using System.Collections;
using UnityEngine;

public class BlackHoleGenerator : MonoBehaviour
{
    public GameObject blackHolePrefab;  // 블랙홀 프리팹
    public float spawnInterval = 1f;    // 블랙홀 생성 간격
    public float minX = -5f;            // X축 최소값
    public float maxX = 5f;             // X축 최대값
    public float spawnHeight = 10f;     // 블랙홀 생성 높이

    void Start()
    {
        // 블랙홀 생성 시작
        StartCoroutine(SpawnBlackHoles());
    }

    // 블랙홀 생성하는 코루틴
    private IEnumerator SpawnBlackHoles()
    {
        while (true) // 무한 루프, 계속 생성
        {
            // 일정 시간 간격으로 블랙홀 생성
            SpawnBlackHole();
            yield return new WaitForSeconds(spawnInterval); // spawnInterval 시간 간격으로 생성
        }
    }

    // 블랙홀 생성 메소드
    private void SpawnBlackHole()
    {
        // 블랙홀 프리팹을 지정된 위치에 생성
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), spawnHeight, 0f);
        Instantiate(blackHolePrefab, spawnPosition, Quaternion.identity);
    }
}
