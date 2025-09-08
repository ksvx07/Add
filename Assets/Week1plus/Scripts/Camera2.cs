using UnityEngine;

public class Camera2 : MonoBehaviour
{
    public float rotateSpeed = 100f;
    private float minX = -45f; // 아래로 최대
    private float maxX = 45f;  // 위로 최대

    void Update()
    {
        Vector3 euler = transform.rotation.eulerAngles;

        // y축 회전
        if (Input.GetKey(KeyCode.RightArrow))
            euler.y += rotateSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.LeftArrow))
            euler.y -= rotateSpeed * Time.deltaTime;

        // y축 값 0~360 범위로 정규화
        euler.y = Mathf.Repeat(euler.y, 360f);

        // x축 0~360 → -180~180 변환
        if (euler.x > 180f) euler.x -= 360f;

        // x축 회전
        if (Input.GetKey(KeyCode.UpArrow))
            euler.x -= rotateSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.DownArrow))
            euler.x += rotateSpeed * Time.deltaTime;

        // x축 제한
        euler.x = Mathf.Clamp(euler.x, minX, maxX);

        transform.rotation = Quaternion.Euler(euler);
    }
}