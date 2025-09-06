using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class GridMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gridSize = 1f;
    private bool isMoving = false;
    private Rigidbody rb;


    [SerializeField] private float jumpHeight = 1f;   // ���� �ִ� ����
    [SerializeField] private float jumpDuration = 0.5f; // ���� �ð�
    [SerializeField] private float moveDistance = 2f;  // �̵� �Ÿ� (2ĭ)

    private bool isJumping = false;
    private float elapsed = 0f;
    private Vector3 startPos;
    private Vector3 endPos;
    private Gravity gravity;

    [SerializeField] private Transform roomPivot;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gravity = GetComponent<Gravity>();
    }

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W)) StartCoroutine(Move(Vector3.forward));
            if (Input.GetKeyDown(KeyCode.S)) StartCoroutine(Move(Vector3.back));
            if (Input.GetKeyDown(KeyCode.A)) StartCoroutine(Move(Vector3.left));
            if (Input.GetKeyDown(KeyCode.D)) StartCoroutine(Move(Vector3.right));
        }

        // ���� ���̸� �Է� ����
        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            StartJump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ReverseRoom();
        }
    }

    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        gravity?.EnableGravity(false);
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + direction * gridSize;

        float elapsed = 0f;
        while (elapsed < 1f)
        {
            rb.MovePosition(Vector3.Lerp(startPos, endPos, elapsed));
            elapsed += Time.deltaTime * moveSpeed;
            yield return null;
        }

        rb.MovePosition(endPos); // ����
        isMoving = false;
        gravity?.EnableGravity(true);
    }


    void FixedUpdate()
    {
        if (!isJumping) return;

        elapsed += Time.fixedDeltaTime;
        float t = Mathf.Clamp01(elapsed / jumpDuration);

        // ������ ���: y = -4h*(t-0.5)^2 + h
        float y = (-4f * jumpHeight * (t - 0.5f) * (t - 0.5f) + jumpHeight);

        // �������� 2ĭ �̵� + y ������
        Vector3 targetPos = Vector3.Lerp(startPos, endPos, t);
        targetPos.y += y;

        rb.MovePosition(targetPos);

        if (t >= 1f)
        {
            isJumping = false;
            gravity?.EnableGravity(true); // ���� ������ �߷� ��
        }
    }

    void StartJump()
    {
        isJumping = true;
        elapsed = 0f;
        gravity?.EnableGravity(false); // ���� �� �߷� ����

        startPos = rb.position;
        endPos = startPos + transform.forward * moveDistance;
    }

    void ReverseRoom()
    {
        roomPivot.Rotate(new Vector3(0f, 0f, 180f));
    }
}
