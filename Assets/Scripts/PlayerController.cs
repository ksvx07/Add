using UnityEngine;

public class PlayerController : SingletonObject<PlayerController>
{
    private Transform cameraTransform => CameraMove.Instance.transform;
    private Rigidbody rb => GetComponent<Rigidbody>();
    private bool isGrounded = false;
    public int jumpCount = GameConstant.maxJumpCount;
    private Vector3 savePoint;

    protected override void Awake()
    {
        //Mingku87_GameManager.Instance.OnGameReset += Reset;
    }

    void Update()
    {
        if (GameManager.canMove == false) return;

        Move();
        Jump();
        ApplyGravity();
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = (forward * moveZ + right * moveX).normalized;
        Vector3 velocity = moveDir * GameConstant.playerSpeed;

        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

        Debug.Log(rb.linearVelocity);
    }

    private void Jump()
    {
        if (jumpCount == 0) return;
        if (Input.GetKeyDown(KeyCode.Space) == false) return;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, GameConstant.playerJumpForce, rb.linearVelocity.z);
        jumpCount--;
        isGrounded = false;
    }

    void ApplyGravity()
    {
        if (isGrounded == true) return;

        float multiplier = rb.linearVelocity.y > 0 ?
            GameConstant.playerGravityMultiplier : GameConstant.playerFallMultiplier;
        rb.linearVelocity += Vector3.up * Physics.gravity.y * multiplier * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.groundTag))
        {
            isGrounded = true;
            jumpCount = GameConstant.maxJumpCount;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConstant.groundTag))
        {
            isGrounded = false;
        }
    }

    public void Reset() => transform.position = savePoint;
    public void SetSavePoint(Vector3 savePoint) => this.savePoint = savePoint;
}