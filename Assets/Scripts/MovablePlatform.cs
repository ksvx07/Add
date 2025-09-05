using UnityEngine;
using System.Collections;

public class MovablePlatform : MonoBehaviour
{
    public float moveSpeed = 2f;   // 이동 속도
    public float gridSize = 1f;    // 한 칸 크기
    private bool isMoving = false; // 이동 중인지 체크

    public void Move()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveOneTile(Vector3.forward)); // 예시: 앞으로 한 칸
        }
    }

    private IEnumerator MoveOneTile(Vector3 direction)
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + direction * gridSize;

        float elapsed = 0f;
        while (elapsed < 1f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed);
            elapsed += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = endPos; // 정확히 끝 위치 보정
        isMoving = false;
    }
}
