using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gridSize = 1f;
    private bool isMoving = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    }

    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + direction * gridSize;

        float elapsed = 0f;
        while (elapsed < 1f)
        {
            rb.MovePosition(Vector3.Lerp(startPos, endPos, elapsed));
            elapsed += Time.deltaTime * moveSpeed;
            yield return null;
        }

        rb.MovePosition(endPos); // º¸Á¤
        isMoving = false;
    }
}
