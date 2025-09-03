using UnityEngine;

public class LeverInteraction : MonoBehaviour
{

    public Animator leverAnimator;
    public Animator doorAnimator;

    private bool canInteract = false;
    private bool isPulled = false;

    void Update()
    {
        if (canInteract && !isPulled && Input.GetKeyUp(KeyCode.E))
        {
            PullLever();
        }
    }
    void PullLever()
    {
        Debug.Log("레버를 당겼습니다.");
        isPulled = true;

        leverAnimator.SetTrigger("Pull");
        doorAnimator.SetTrigger("Open");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            Debug.Log("플레이어가 레버 범위에 들어왔습니다.");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            canInteract = false ;
            Debug.Log("플레이어가 레버 범위에서 나갔습니다.");
        }

    }

}
