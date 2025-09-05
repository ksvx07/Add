using UnityEngine;

public class Laser : MonoBehaviour
{
    private float maxDistance = 10f; // ������ ����
    private LineRenderer lr;

    public bool blink = true;
    public float onTime = 2f;   // ���� �ִ� �ð�
    public float offTime = 2f;  // ���� �ִ� �ð�

    private float timer = 0f;
    private bool isOn = true;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = true; // ���� �� ����
    }

    void Update()
    {
        if (blink)
        {
            HandleBlink();
        }

        if (isOn)
        {
            ShootLaser();
        }
    }

    void HandleBlink()
    {
        timer += Time.deltaTime;

        if (isOn && timer >= onTime)
        {
            isOn = false;
            lr.enabled = false; // ������ ����
            timer = 0f;
        }
        else if (!isOn && timer >= offTime)
        {
            isOn = true;
            lr.enabled = true; // ������ �ѱ�
            timer = 0f;
        }
    }

    void ShootLaser()
    {
        Vector3 startPos = transform.position;
        Vector3 dir = transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(startPos, dir, out hit, maxDistance))
        {
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, hit.point);

            // �÷��̾� ����
            if (hit.collider.CompareTag(GameConstant.playerTag))
            {
                PlayerController.Instance.Die();
            }
        }
        else
        {
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, startPos + dir * maxDistance);
        }
    }
}
