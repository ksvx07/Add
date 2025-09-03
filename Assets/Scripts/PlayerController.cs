using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : SingletonObject<PlayerController>
{
    private Transform cameraTransform => CameraMove.Instance.transform;
    private Rigidbody rb => GetComponent<Rigidbody>();
    private bool isGrounded = false;
    public int jumpCount = GameConstant.maxJumpCount;
    private int jumpLimitCount = GameConstant.playerJumpLimitCount;
    [SerializeField] private GameObject jumpLimitCountText;
    [SerializeField] private Vector3 savePoint = new Vector3(0, 1, 0);
    private float stopTimer;
    private bool hasBeenMove;
    private bool isMovingReverse = false;

    protected override void Awake()
    {
        base.Awake();

        GameManager.Instance.OnStageStart += Initialize;
        GameManager.Instance.OnStageRestart += Restart;
    }

    void Update()
    {
        if (GameManager.canMove == false) return;

        LookForward();
        Move();
        Jump();
        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.L)) GameManager.Instance.StartStage();
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager.Instance.ClearStage();
            GameManager.Instance.StartStage();
        }
    }

    private void LookForward()
    {
        Vector3 dir = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        if (dir.sqrMagnitude < 0.01f) return;
        quaternion target = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, 50 * Time.deltaTime);
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        if (isMovingReverse == true) moveX *= -1;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = (forward * moveZ + right * moveX).normalized;
        Vector3 velocity = moveDir * GameConstant.playerSpeed;

        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

        if (transform.position.y < -10) Die();
        if (GameManager.stage < GameConstant.playerDieWhenStopStage) return;

        if (moveDir == Vector3.zero) stopTimer -= Time.deltaTime;
        else
        {
            hasBeenMove = true;
            stopTimer = GameConstant.playerDieTimer;
        }

        if (stopTimer <= 0 && hasBeenMove == true) Die();
    }

    private void Jump()
    {
        if (jumpCount == 0) return;
        if (jumpLimitCount == 0) return;
        if (Input.GetKeyDown(KeyCode.Space) == false) return;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, GameConstant.playerJumpForce, rb.linearVelocity.z);
        jumpCount--;
        isGrounded = false;

        if (GameManager.stage < GameConstant.playerJumpLimitStage) return;
        jumpLimitCount--;
        jumpLimitCountText.GetComponent<TextMeshPro>().text = jumpLimitCount.ToString();
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

    public void Restart()
    {
        Initialize();

        rb.linearVelocity = Vector3.zero;
        transform.position = savePoint;
    }

    public void Initialize()
    {
        jumpLimitCount = GameConstant.playerJumpLimitCount;
        stopTimer = GameConstant.playerDieTimer;
        hasBeenMove = false;

        if (GameManager.stage < GameConstant.playerJumpLimitStage) return;
        jumpLimitCountText.SetActive(true);

        if (GameManager.stage < GameConstant.playerMoveFlipStage) return;
        isMovingReverse = true;
    }

    public void Die()
    {
        GameManager.Instance.Restart();
    }
}