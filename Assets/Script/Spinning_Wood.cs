using UnityEngine;

public class Spinning_Wood : MonoBehaviour
{
    public float rotationSpeed = 100f; // ȸ�� �ӵ�

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ��������
        rb.angularVelocity = rotationSpeed; // ȸ�� �ӵ� ����
    }

    void Update()
    {
        // ȸ���� ���� Rigidbody2D�� angularVelocity�� ó��
        // transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // �� �κ��� ����
    }
}
