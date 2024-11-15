using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ���� ��� (�÷��̾�)
    public float smoothSpeed = 0.125f; // ī�޶� �̵��� �ε巯�� ����
    public Vector3 offset; // ī�޶�� ��� ���� �Ÿ� ������

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned to the CameraFollow script.");
            return;
        }

        // ��ǥ ��ġ ���
        Vector3 desiredPosition = target.position + offset;

        // Lerp�� ����Ͽ� �ε巴�� �̵�
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // ī�޶� ��ġ ����
        transform.position = smoothedPosition;
    }
}
