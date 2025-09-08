using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GridMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gridSize = 1f;

    private Vector3 moveDir = Vector3.forward;
    private Vector3 prePos = Vector3.zero;
    private Animator animator;

    [SerializeField] private float moveDistance = 2f;  // 이동 거리 (2칸)

    [SerializeField] private float rayMaxDis = 0.6f;

    [SerializeField] private Transform roomPivot;

    private bool dashReady;
    private float dashRayMaxDis = 5.0f;
    private bool dashing;
    private Vector3 dashPos = Vector3.zero;
    [SerializeField] private float dashSpeed = 5.0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // MoveRay 그린거임 필요없으면 지우셈
        //Debug.DrawRay(transform.position + new Vector3(0f, 0.25f, 0f), transform.forward * rayMaxDis, Color.red);
        //Debug.DrawRay(transform.position - new Vector3(0f, 0.25f, 0f), transform.forward * rayMaxDis, Color.red);
        //Debug.DrawRay(transform.position + new Vector3(0f, 0.75f, 0f), transform.forward * rayMaxDis, Color.red);

        if (!GameManager2.instance.busy)
        {
            if (Input.GetKeyDown(KeyCode.W)) Move(0);
            if (Input.GetKeyDown(KeyCode.S)) Move(180);
            if (Input.GetKeyDown(KeyCode.A)) Move(270);
            if (Input.GetKeyDown(KeyCode.D)) Move(90);

            if (Input.GetKeyDown(KeyCode.E)) ReadyDash();
        }
        else
        {
            if (dashReady)
            {
                if (Input.GetKeyDown(KeyCode.W)) Dash(0);
                if (Input.GetKeyDown(KeyCode.S)) Dash(180);
                if (Input.GetKeyDown(KeyCode.A)) Dash(270);
                if (Input.GetKeyDown(KeyCode.D)) Dash(90);
            }
        }

        if (dashing)
        {
            CheckDashing();
            ImDashing();
        }
    }

    private void CheckDashing()
    {
        if (Vector3.Distance(transform.position, dashPos) < 0.001f)
        {
            transform.position = dashPos;
            DashEnd();
        }
    }
    private void ImDashing()
    {
        transform.position = Vector3.MoveTowards(transform.position, dashPos, dashSpeed * Time.deltaTime);
    }

    private void ReadyDash()
    {
        GameManager2.instance.StartAction();
        GameManager2.instance.EnableGravity(false);
        dashReady = true;
    }

    private void Dash(int dashDirRot)
    {
        Vector3 dashDir = dashDirRot switch
        {
            0 => Vector3.forward,
            180 => Vector3.back,
            270 => Vector3.left,
            90 => Vector3.right,
            _ => Vector3.forward
        };
        // 여기서 방향 잡아주고 아래에서 레이 쏴서 가능한지 확인
        Vector3 euler = transform.rotation.eulerAngles;
        euler.y = dashDirRot;  // z만 변경
        transform.rotation = Quaternion.Euler(euler);

        float dashDistance = FindDashDis();

        dashDistance -= 0.5f;
        dashDistance = Mathf.Round(dashDistance);


        if (dashDistance == 0)
        {
            Blocked();// 벽박은 애니메이션
            DashEnd();
        }
        else
        {
            dashReady = false;
            dashPos = transform.position + dashDir * dashDistance;
            dashing = true;

        }
    }
    
    private void DashEnd()
    {
        dashReady = false;
        dashing = false;
        GameManager2.instance.EnableGravity(true);
        GameManager2.instance.EndAction();
    }

    private float FindDashDis()
    {
        float dashDistance = dashRayMaxDis;
        Vector3 pos = transform.position;
        Vector3 upRayPos = pos + new Vector3(0f, 0.25f, 0f);
        Vector3 downRayPos = pos - new Vector3(0f, 0.25f, 0f);
        if (Physics.Raycast(upRayPos, transform.forward, out RaycastHit hit, dashRayMaxDis))
            dashDistance = hit.distance;
        if (Physics.Raycast(downRayPos, transform.forward, out hit, dashRayMaxDis))
            if (hit.distance < dashDistance)
                dashDistance = hit.distance;

        return dashDistance;
    }

    private bool MoveRay()
    {
        Vector3 pos = transform.position;
        Vector3 upRayPos = pos + new Vector3(0f, 0.25f, 0f);
        Vector3 downRayPos = pos - new Vector3(0f, 0.25f, 0f);
        Vector3 topFrontRayPos = pos + new Vector3(0f, 0.75f, 0f);

        if (Physics.Raycast(upRayPos, transform.forward, rayMaxDis) 
            || Physics.Raycast(downRayPos, transform.forward, rayMaxDis)
            || Physics.Raycast(topFrontRayPos, transform.forward, rayMaxDis)
            || Physics.Raycast(pos, Vector3.up, rayMaxDis))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Move(int moveDirRot)
    {
        moveDir = moveDirRot switch
        {
            0 => Vector3.forward,
            180 => Vector3.back,
            270 => Vector3.left,
            90 => Vector3.right,
            _ => Vector3.forward
        };
        // 여기서 방향 잡아주고 아래에서 레이 쏴서 가능한지 확인
        Vector3 euler = transform.rotation.eulerAngles;
        euler.y = moveDirRot;  // z만 변경
        transform.rotation = Quaternion.Euler(euler);

        // 가능한지 확인
        bool canMove = MoveRay();

        if (canMove)
        {
            prePos = transform.position;
            animator.SetTrigger("Forward");
            GameManager2.instance.StartAction();
            GameManager2.instance.EnableGravity(false);
        }
        else
        {
            Blocked();
            Debug.Log("Can't move");
        }
    }
    
    public void MoveEnd()
    {
        animator.Play("Nothing");
        prePos += moveDir;
        transform.position = prePos;
        GameManager2.instance.EndAction();
        GameManager2.instance.EnableGravity(true);
    }

    public void Blocked()
    {
        // Add Blocked animation
    }
}
