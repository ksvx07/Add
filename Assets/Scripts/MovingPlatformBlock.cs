using UnityEngine;

public class MovingPlatformBlock : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.playerTag))
        {
            other.transform.SetParent(parent.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConstant.playerTag))
        {
            other.transform.SetParent(null);
        }
    }
}