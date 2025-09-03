using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField] private bool isLoop = false;
    [SerializeField] private BoxCollider trigger;
    private bool hasWork;
    private Animator animator => GetComponent<Animator>();

    void Awake()
    {
        GameManager.Instance.OnStageStart += Initialize;
        GameManager.Instance.OnStageRestart += Initialize;

        Initialize();
    }

    public void Initialize()
    {
        if (GameManager.stage < GameConstant.startSpikeTrapStage) return;

        hasWork = false;
        animator.SetTrigger("ResetSpike");

        trigger.enabled = !isLoop;
        animator.SetBool("isLoop", isLoop);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isLoop || hasWork) return;
        if (other.CompareTag(GameConstant.playerTag) == false) return;

        hasWork = true;
        trigger.enabled = false;
        animator.ResetTrigger("ResetSpike");
        animator.SetTrigger("SpikeMove");
    }
}