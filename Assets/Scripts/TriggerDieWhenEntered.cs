using UnityEngine;

public class TriggerDieWhenEntered : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GameManager.Instance.Restart();
            }
        }
    }
}