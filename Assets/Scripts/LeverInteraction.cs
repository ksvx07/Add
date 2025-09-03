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
        Debug.Log("������ �����ϴ�.");
        isPulled = true;

        leverAnimator.SetTrigger("Pull");
        doorAnimator.SetTrigger("Open");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            Debug.Log("�÷��̾ ���� ������ ���Խ��ϴ�.");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            canInteract = false ;
            Debug.Log("�÷��̾ ���� �������� �������ϴ�.");
        }

    }

}
