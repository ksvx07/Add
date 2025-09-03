using UnityEngine;

public class Ksvx07_PingPong : MonoBehaviour
{
    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    public Vector3 moveDir;

    private Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        transform.position = startPos + moveDir * offset;
    }
}
