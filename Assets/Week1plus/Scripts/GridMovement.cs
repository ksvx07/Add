using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gridSize = 1f;
    private bool isMoving = false;

    [SerializeField] private float jumpHeight = 1f;   // 점프 최대 높이
    [SerializeField] private float jumpDuration = 0.5f; // 점프 시간
    [SerializeField] private float moveDistance = 2f;  // 이동 거리 (2칸)

    private bool isJumping = false;
    private float elapsed = 0f;
    private Vector3 startPos;
    private Vector3 endPos;
    private Gravity gravity;

    [SerializeField] private Transform roomPivot;

    void Awake()
    {
        gravity = GetComponent<Gravity>();
    }

    void Update()
    {
        if (!isMoving && !isJumping)
        {
            if (Input.GetKeyDown(KeyCode.W)) StartCoroutine(Move(Vector3.forward));
            if (Input.GetKeyDown(KeyCode.S)) StartCoroutine(Move(Vector3.back));
            if (Input.GetKeyDown(KeyCode.A)) StartCoroutine(Move(Vector3.left));
            if (Input.GetKeyDown(KeyCode.D)) StartCoroutine(Move(Vector3.right));
        }

        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            StartJump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ReverseRoom();
        }
    }

    //IEnumerator Move(Vector3 direction)
    //{
    //    isMoving = true;
    //    gravity?.EnableGravity(false);

    //    Vector3 start = transform.position;
    //    Vector3 end = start + direction * gridSize;

    //    float elapsedTime = 0f;
    //    while (elapsedTime < 1f)
    //    {
    //        transform.position = Vector3.Lerp(start, end, elapsedTime);
    //        elapsedTime += Time.deltaTime * moveSpeed;
    //        yield return null;
    //    }

    //    transform.position = end; // 보정
    //    isMoving = false;
    //    gravity?.EnableGravity(true);
    //}

    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        gravity?.EnableGravity(false);

        Vector3 start = transform.position;
        Vector3 end = start + direction * gridSize;

        // 레이캐스트로 Pushable 체크
        RaycastHit hit;
        GameObject pushable = null;
        if (Physics.Raycast(start, direction, out hit, gridSize))
        {
            if (hit.collider.CompareTag("Pushable"))
            {
                pushable = hit.collider.gameObject;
            }
        }

        Vector3 pushableStart = Vector3.zero;
        Vector3 pushableEnd = Vector3.zero;
        if (pushable != null)
        {
            pushableStart = pushable.transform.position;
            pushableEnd = pushableStart + direction * gridSize;
        }

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            float t = elapsedTime;

            transform.position = Vector3.Lerp(start, end, t);

            if (pushable != null)
            {
                pushable.transform.position = Vector3.Lerp(pushableStart, pushableEnd, t);
            }

            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = end; // 플레이어 위치 보정
        if (pushable != null)
            pushable.transform.position = pushableEnd; // Pushable 위치 보정

        isMoving = false;
        gravity?.EnableGravity(true);
    }


    void FixedUpdate()
    {
        if (!isJumping) return;

        elapsed += Time.fixedDeltaTime;
        float t = Mathf.Clamp01(elapsed / jumpDuration);

        // 포물선 계산: y = -4h*(t-0.5)^2 + h
        float y = (-4f * jumpHeight * (t - 0.5f) * (t - 0.5f) + jumpHeight);

        Vector3 targetPos = Vector3.Lerp(startPos, endPos, t);
        targetPos.y += y;

        transform.position = targetPos;

        if (t >= 1f)
        {
            isJumping = false;
            gravity?.EnableGravity(true); // 점프 끝나면 중력 켬
        }
    }

    void StartJump()
    {
        isJumping = true;
        elapsed = 0f;
        gravity?.EnableGravity(false);

        startPos = transform.position;
        endPos = startPos + transform.forward * moveDistance;
    }

    void ReverseRoom()
    {
        roomPivot.Rotate(new Vector3(0f, 0f, 180f));
    }
}
