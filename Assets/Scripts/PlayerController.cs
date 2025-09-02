using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    public float walkSpeed = 12.0f;
    public float gravity = -9.81f;
    public float jumpVelocity = 5f;

    public Vector3 velocity;
    
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    public float groundBuffer;
    private float groundBufferSet = 0.25f;
    private float jumpKeyBuffer;
    private float jumpKeyBufferSet = 0.1f;
    private bool isGrounded = false;

    public Vector3 respawnPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // check ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        groundBuffer = isGrounded ? groundBufferSet : groundBuffer - Time.deltaTime;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        // move
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;

        characterController.Move(moveDirection * walkSpeed * Time.deltaTime);

        // jump
        jumpKeyBuffer = Input.GetButtonDown("Jump") ? jumpKeyBufferSet : jumpKeyBuffer - Time.deltaTime;

        if (jumpKeyBuffer > 0 && groundBuffer > 0)
        {
            velocity.y = jumpVelocity;
            isGrounded = false;
            groundBuffer = 0;
            jumpKeyBuffer = 0;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathTrigger"))
        {
            LolYouAreDead();
        }
        
        if (other.gameObject.CompareTag("CheckPointTrigger"))
        {
            respawnPosition = other.gameObject.transform.position;
        }
    }

    public void LolYouAreDead()
    {
        // not big surprise
        characterController.enabled = false;
        transform.position = respawnPosition;
        characterController.enabled = true;
    }
}
