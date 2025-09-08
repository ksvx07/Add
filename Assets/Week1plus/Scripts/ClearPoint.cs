using UnityEngine;

public class ClearPoint : MonoBehaviour
{
    bool clear;
    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.back * 0.6f, Color.red);
        if (!clear)
        {
            if (Physics.Raycast(transform.position, Vector3.back, out RaycastHit hit, 0.6f))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    clear = true;
                    GameManager2.instance.NextLevel();
                }
            }
        }
    }
}
