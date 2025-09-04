using UnityEngine;

public class TeleportToNextLevel : MonoBehaviour
{
    public Transform tpExit;
    public Transform tpEnter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.isClear) return;
        if (other.CompareTag(GameConstant.playerTag))
        {
            other.transform.position = other.transform.position - tpEnter.position + tpExit.position;
        }
    }
}
