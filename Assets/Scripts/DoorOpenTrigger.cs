using UnityEngine;

public class DoorOpenTrigger : SingletonObject<DoorOpenTrigger>
{
    public Animator doorAnimator;
    public static bool hasTrigged = false;

    protected override void Awake()
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
        hasTrigged = false;
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
