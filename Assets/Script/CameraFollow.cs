using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 따라갈 대상 (플레이어)
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움 정도
    public Vector3 offset; // 카메라와 대상 간의 거리 오프셋

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned to the CameraFollow script.");
            return;
        }

        // 목표 위치 계산
        Vector3 desiredPosition = target.position + offset;

        // Lerp를 사용하여 부드럽게 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 카메라 위치 갱신
        transform.position = smoothedPosition;
    }
}
