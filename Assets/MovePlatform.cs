using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;
    private float rayDistance = 0.6f;


    private static readonly Vector3[] directions = new Vector3[]
    {
        Vector3.forward,
        Vector3.back,
        Vector3.left,
        Vector3.right,
        Vector3.up,
        Vector3.down
    };

    private bool[] wasHitLastFrame = new bool[6];


    private void Move()
    {
        foreach (GameObject platform in platforms)
        {
            MovablePlatform mp = platform.GetComponent<MovablePlatform>();
            if (mp != null)
            {
                mp.Move();
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            bool isHitNow = Physics.Raycast(transform.position, directions[i], out RaycastHit hit, rayDistance);
            if (isHitNow && !wasHitLastFrame[i])
            {
                wasHitLastFrame[i] = isHitNow;
                if (hit.collider.CompareTag(GameConstant.playerTag))
                {
                    Move();
                    break;
                }
            }
            else
            {
                wasHitLastFrame[i] = isHitNow;
            }
            
        }
    }
}