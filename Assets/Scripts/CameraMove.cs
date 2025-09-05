using UnityEngine;

public class CameraMove : SingletonObject<CameraMove>
{
    private Vector3 offset = new Vector3(0, 3, -3);
    [SerializeField]private Transform target;
    //private Transform target => PlayerController.Instance.transform;

    private const float sensitivity = 3f;

    private float startYaw;
    private float startPitch;

    private float yaw = 0f;
    private float pitch = 0f;
    private const float minPitch = -40f;
    private const float maxPitch = 10f;

    Vector3 desiredPosition;

    protected override void Awake()
    {
        base.Awake();

        startYaw = transform.eulerAngles.y;
        startPitch = transform.eulerAngles.x;

        GameManager.Instance.OnStageRestart += Initialize;
    }

    public void Initialize()
    {
        yaw = startYaw;
        pitch = startPitch;
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
        float maxDistanceCameraToPlayer = desiredPosition.magnitude;

        Ray ray = new Ray(target.position, desiredPosition - target.position);
        Debug.DrawLine(target.position, desiredPosition, Color.red);
        RaycastHit hitInfo;
        float rayCastLength = (target.position - desiredPosition).magnitude;
        bool hasRayHit = Physics.Raycast(ray, out hitInfo, rayCastLength);
        if (hasRayHit)
        {
            desiredPosition = hitInfo.point;
        }

        transform.position = desiredPosition;//target.position + Quaternion.Euler(pitch, yaw, 0) * offset;

        float offsetAmount = hasRayHit ? hitInfo.distance / rayCastLength : 1.0f; // this returns infinite
        transform.LookAt(target.position + (Vector3.up * (1.5f * offsetAmount)));
    }
}