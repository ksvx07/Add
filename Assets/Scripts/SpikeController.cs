using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField] private bool isLoop = false;
    private bool hasWork;
    private Animator animator => GetComponent<Animator>();

    void Awake()
    {
        GameManager.Instance.OnStageStart += Initialize;
        GameManager.Instance.OnStageRestart += Initialize;
    }

    public void Initialize()
    {
        hasWork = false;
        animator.SetTrigger("ResetSpike");

        if (isLoop) animator.SetBool("isLoop", true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isLoop == false || hasWork) return;
        if (other.CompareTag(GameConstant.playerTag) == false) return;

        animator.ResetTrigger("ResetSpike");
        animator.SetTrigger("SpikeMove");
    }
}