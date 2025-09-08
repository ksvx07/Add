using System.Linq;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public MovablePlatform platform;
    private float rayDistance = 0.35f;
    private bool wasHitLastFrame = false;
    private bool on;
    [SerializeField] private Transform child;

    //private void Start()
    //{
    //    if (platform == null) platform = GameObject.FindWithTag("Movable")?.GetComponent<MovablePlatform>();
    //}


    private void Move()
    {
        if (platform == null) return; // 플랫폼이 아직 없으면 무시
        // 안전할 때만 실행
        platform.MoveStart();
    }

    private void Update()
    {
        bool isHitNow = Physics.Raycast(child.position, Vector3.up, rayDistance);

        // 이전 프레임에는 안 맞았는데 이번 프레임에 맞음 → "막 맞았다!"
        if (!wasHitLastFrame && isHitNow)
        {
            on = true;
        }

        // 상태 갱신
        wasHitLastFrame = isHitNow;

        // 디버그 라인 그리기
        Debug.DrawRay(child.position, Vector3.up * rayDistance, Color.red);

        if (on && !GameManager2.instance.busy)
        {
            on = false;
            Move();
        }
    }
}