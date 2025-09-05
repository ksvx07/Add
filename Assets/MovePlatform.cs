using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;

    private void OnCollisionEnter(Collision collision)
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
}