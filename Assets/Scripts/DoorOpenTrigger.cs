using System.Xml.Serialization;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    public Animator doorAnimator;

    private bool hasTrigged = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTrigged)
        {
            doorAnimator.SetTrigger("Open");
            GameManager.Instance.StartStage();

            hasTrigged = true;
        }
    }
}
