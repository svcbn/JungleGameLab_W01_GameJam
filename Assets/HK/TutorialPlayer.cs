using UnityEngine;

public class TutorialPlayer : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 이동 속도 조절 변수

    private Rigidbody2D rb;
    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // WASD 키 입력 처리
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = moveDirection * moveSpeed;

        Vector3 playerPosition = transform.position;
        playerPosition.z = mainCamera.transform.position.z;
        mainCamera.transform.position = playerPosition;
    }
}