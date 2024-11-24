using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectGenerator : MonoBehaviour
{
    private bool isCooldown;        // 쿨타임 여부 확인
    private float cooldownDuration = 3f; // 쿨타임 지속 시간

    public GameObject fallingObjectPrefab;
    public int objectCount = 5; // 한 번에 떨어뜨릴 낙하물의 개수
    public float spawnInterval = 0.5f; // 낙하물 간 생성 간격
    public float minDistance = 2f; // 낙하물 간 최소 거리

    private List<Vector3> spawnPositions = new List<Vector3>(); // 이미 사용된 위치 저장

    void Update()
    {
        if (!isCooldown)
        {
            StartCoroutine(SpawnObjectsSequentially());
            StartCooldown();
        }
    }

    IEnumerator SpawnObjectsSequentially()
    {
        spawnPositions.Clear(); // 기존 위치 초기화

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 newPosition = GetUniqueRandomPosition();
            Instantiate(fallingObjectPrefab, newPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval); // 생성 간격 대기
        }
    }

    void StartCooldown()
    {
        isCooldown = true; // 쿨타임 활성화
        Invoke(nameof(ResetCooldown), cooldownDuration); // 쿨타임 종료 예약
    }

    void ResetCooldown()
    {
        isCooldown = false; // 쿨타임 종료
    }

    Vector3 GetUniqueRandomPosition()
    {
        Vector3 position;
        int maxAttempts = 10; // 무한 루프 방지용 최대 시도 횟수
        int attempts = 0;

        do
        {
            float randomX = Random.Range(-4f, 41f);
            position = new Vector3(randomX, 9, 0);

            attempts++;
        } while (!IsPositionValid(position) && attempts < maxAttempts);

        spawnPositions.Add(position); // 사용된 위치 저장
        return position;
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (var usedPosition in spawnPositions)
        {
            if (Vector3.Distance(position, usedPosition) < minDistance)
            {
                return false; // 위치가 너무 가까움
            }
        }
        return true;
    }
}
