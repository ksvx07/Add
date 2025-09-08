using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private float gravitySpeed = 3f;
    [SerializeField] private float rayLength = 0.6f;
    private float stepUnit = 0.5f;

    //private bool gravityEnabled = true;
    private bool isFalling = false;
    private Vector3 targetPos = Vector3.zero;

    private bool IsGrounded;

    private void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.down* rayLength, Color.blue);

        if (!GameManager2.instance.gravityEnabled) return;
        if (!isFalling)
        {
            ApplyGravity();
        }
        else
        {
            fall();
            checkFalling();
        }

        
    }

    private void ApplyGravity()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength);

        if (!IsGrounded)
        {
            targetPos = transform.position + Vector3.down * stepUnit;
            isFalling = true;
            GameManager2.instance.StartAction();
        }
            //transform.position += Vector3.down * gravitySpeed * Time.deltaTime;
    }

    private void fall()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, gravitySpeed * Time.deltaTime);
    }

    private void checkFalling()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.001f)
        {
            transform.position = targetPos;
            isFalling = false;
            GameManager2.instance.EndAction();
        }
    }

}