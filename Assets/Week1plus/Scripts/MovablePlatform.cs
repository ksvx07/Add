using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    private float moveSpeed = 5f;   // 이동 속도
    public float gridSize = 1f;    // 한 칸 크기
    [SerializeField] private float rayMaxDis = 0.6f;

    private bool isMoving;
    private Vector3 targetPos = Vector3.zero;

    private void Update()
    {
        if (isMoving)
        { 
            Move();
            checkMoving();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    private void checkMoving()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.001f)
        {
            isMoving = false;
            GameManager2.instance.EndAction();
            GameManager2.instance.EnableGravity(true);
        }
    }

    private bool MoveRay()
    {
        Vector3 pos = transform.position;
        Vector3 upRayPos = pos + new Vector3(0f, 0.25f, 0f);
        Vector3 downRayPos = pos - new Vector3(0f, 0.25f, 0f);

        if (Physics.Raycast(upRayPos, transform.forward, rayMaxDis)
            || Physics.Raycast(downRayPos, transform.forward, rayMaxDis))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void MoveStart()
    {
        bool canMove = MoveRay();

        if (canMove)
        {
            GameManager2.instance.StartAction();
            GameManager2.instance.EnableGravity(false);
            targetPos = transform.position + Vector3.forward;
            isMoving = true; 
        }
        else
        {
            Blocked();
        }

    }

    private void Blocked()
    {

    }
}


