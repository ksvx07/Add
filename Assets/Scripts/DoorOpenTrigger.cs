using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    public Animator doorAnimator;

    private bool hasTrigged = false;

    void Awake()
    {
        GameManager.Instance.OnStageStart += Initialize;
        GameManager.Instance.OnStageRestart += Reset;
    }

    public void Initialize()
    {
        hasTrigged = false;
    }

    public void Reset()
    {
        doorAnimator.SetTrigger("Close");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTrigged)
        {
            doorAnimator.ResetTrigger("Close");
            doorAnimator.SetTrigger("Open");
            GameManager.Instance.StartStage();

            hasTrigged = true;
        }
    }
}
