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

    Vector3 desiredPosition;

    void Start()
    {
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        float mouseX = GameManager.canMove ? Input.GetAxis("Mouse X") * sensitivity : 0;
        float mouseY = GameManager.canMove ? Input.GetAxis("Mouse Y") * sensitivity : 0;

        yaw += mouseX;
        pitch -= mouseY;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        desiredPosition = target.position + Quaternion.Euler(pitch, yaw, 0) * offset;

        Ray ray = new Ray(target.position, desiredPosition - target.position);
        Debug.DrawLine(target.position, desiredPosition, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, (target.position - desiredPosition).magnitude))
        {
            desiredPosition = hitInfo.point;
        }

        transform.position = desiredPosition;//target.position + Quaternion.Euler(pitch, yaw, 0) * offset;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}