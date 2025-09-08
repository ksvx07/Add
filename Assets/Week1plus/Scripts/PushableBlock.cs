using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    [SerializeField] private float rayMaxDis = 0.6f;
    private Vector3 moveDir = Vector3.forward;
    private Vector3 prePos = Vector3.zero;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }



    public void Pushed(int moveDirRot)
    {
        Move(moveDirRot);

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

    private void Blocked()
    {

    }

    public void MoveEnd()
    {
        animator.Play("Nothing");
        prePos += moveDir;
        transform.position = prePos;
        GameManager2.instance.EndAction();
        GameManager2.instance.EnableGravity(true);
    }


    private bool MoveRay()
    {
        Vector3 pos = transform.position;
        Vector3 upRayPos = pos + new Vector3(0f, 0.25f, 0f);
        Vector3 downRayPos = pos - new Vector3(0f, 0.25f, 0f);
        Vector3 topFrontRayPos = pos + new Vector3(0f, 0.75f, 0f);

        RaycastHit hit1, hit2;
        bool hitUp = Physics.Raycast(upRayPos, transform.forward, out hit1, rayMaxDis);
        bool hitDown = Physics.Raycast(downRayPos, transform.forward, out hit2, rayMaxDis);

        if (hitUp
            || hitDown
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
}
