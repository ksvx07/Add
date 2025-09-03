using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{

    public Animator doorAnimator;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            GameManager.Instance.ClearStage();
            doorAnimator.SetTrigger("Close");
            hasTriggered = true;
        }
    }
}