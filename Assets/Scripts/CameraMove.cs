using UnityEngine;

public class CameraMove : SingletonObject<CameraMove>
{
    private Vector3 offset = new Vector3(0, 5, -5);
    private Transform target => PlayerController.Instance.transform;

    private const float sensitivity = 3f;

    private float yaw = 0f;
    private float pitch = 0f;
    private const float minPitch = -40f;
    private const float maxPitch = 10f;

    void Start()
    {
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (GameManager.canMove == false) return;

        Move();
    }

    private void Move()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        yaw += mouseX;
        pitch -= mouseY;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.position = target.position + Quaternion.Euler(pitch, yaw, 0) * offset;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}