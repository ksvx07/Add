using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{

    public Animator doorAnimator;
    private bool hasTriggered = false;

    void Awake()
    {
        GameManager.Instance.OnStageStart += Initialize;
        GameManager.Instance.OnStageRestart += Initialize;
    }

    public void Initialize()
    {
        hasTriggered = false;
        doorAnimator.SetTrigger("Close");
    }

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