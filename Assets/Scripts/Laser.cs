using UnityEngine;

public class Laser : MonoBehaviour
{
    private float maxDistance = 10f; // 레이저 길이
    private LineRenderer lr;

    public bool blink = true;
    public float onTime = 2f;   // 켜져 있는 시간
    public float offTime = 2f;  // 꺼져 있는 시간

    private float timer = 0f;
    private bool isOn = true;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = true; // 시작 시 켜짐
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
            lr.enabled = false; // 레이저 끄기
            timer = 0f;
        }
        else if (!isOn && timer >= offTime)
        {
            isOn = true;
            lr.enabled = true; // 레이저 켜기
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

            // 플레이어 감지
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
