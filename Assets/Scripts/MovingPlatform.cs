using UnityEngine;

public class MovingPlatform : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.playerTag))
        {
            other.transform.SetParent(transform);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConstant.playerTag))
        {
            other.transform.SetParent(null);
        }
    }


}