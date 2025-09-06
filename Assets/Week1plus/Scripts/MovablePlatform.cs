using UnityEngine;
using System.Collections;

public class MovablePlatform : MonoBehaviour
{
    public float moveSpeed = 2f;   // �̵� �ӵ�
    public float gridSize = 1f;    // �� ĭ ũ��
    private bool isMoving = false; // �̵� ������ üũ

    public void Move()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveOneTile(Vector3.forward)); // ����: ������ �� ĭ
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

        transform.position = endPos; // ��Ȯ�� �� ��ġ ����
        isMoving = false;
    }
}
