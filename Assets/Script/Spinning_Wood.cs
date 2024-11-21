using UnityEngine;

public class Spinning_Wood : MonoBehaviour
{
    public float rotationSpeed = 100f; // 회전 속도

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 가져오기
        rb.angularVelocity = rotationSpeed; // 회전 속도 설정
    }

    void Update()
    {
        // 회전은 이제 Rigidbody2D의 angularVelocity로 처리
        // transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // 이 부분은 삭제
    }
}
