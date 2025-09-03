using UnityEngine;

public class LeverInteraction : MonoBehaviour
{

    public Animator leverAnimator;
    public Animator doorAnimator;

    private bool canInteract = false;
    private bool isPulled = false;

    [SerializeField] private GameObject pressEText;

    void Awake()
    {
        GameManager.Instance.OnStageStart += Initialize;
        GameManager.Instance.OnStageRestart += Initialize;
    }

    public void Initialize()
    {
        canInteract = false;
        isPulled = false;
        pressEText.SetActive(false);

        leverAnimator.SetTrigger("Reset");
    }

    void Update()
    {
        if (canInteract && !isPulled && Input.GetKeyUp(KeyCode.E))
        {
            PullLever();
        }
    }
    void PullLever()
    {
        isPulled = true;

        leverAnimator.ResetTrigger("Reset");
        leverAnimator.SetTrigger("Pull");
        doorAnimator.SetTrigger("Open");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.playerTag) == false) return;

        canInteract = true;
        pressEText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConstant.playerTag) == false) return;

        canInteract = false;
        pressEText.SetActive(false);
    }
}