using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private float gravitySpeed = 3f;
    [SerializeField] private float rayLength = 0.6f;
    [SerializeField] private LayerMask groundLayer;

    private bool gravityEnabled = true;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        if (!gravityEnabled) return;

        ApplyGravity();
    }

    private void ApplyGravity()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayer);

        if (!IsGrounded)
            transform.position += Vector3.down * gravitySpeed * Time.deltaTime;
    }

    public void EnableGravity(bool enable) => gravityEnabled = enable;
}