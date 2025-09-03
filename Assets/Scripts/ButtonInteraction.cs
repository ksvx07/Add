using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    [SerializeField] private GameObject spike;
    private Animator animator => GetComponent<Animator>();
    private bool isPressed = false;
    private bool canInteract = false;

    void Awake()
    {
        GameManager.Instance.OnStageStart += Initialize;
        GameManager.Instance.OnStageRestart += Initialize;
    }

    public void Initialize()
    {
        isPressed = false;
        canInteract = false;

        if (GameManager.stage < GameConstant.buttonStage) return;
        spike.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PressButton();
    }
    void PressButton()
    {
        if (isPressed || !canInteract) return;

        isPressed = true;
        animator.SetTrigger("Press");

        if (GameManager.stage < GameConstant.buttonStage)
        {
            PlayerController.Instance.Die();
            return;
        }

        spike.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.playerTag)) canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConstant.playerTag)) canInteract = false;
    }
}