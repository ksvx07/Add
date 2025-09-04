using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    public Vector3 moveDir;

    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        transform.position = startPos + moveDir * offset;
    }
}