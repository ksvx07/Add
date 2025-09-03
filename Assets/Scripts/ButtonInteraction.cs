using Unity.VisualScripting;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public Animator buttonAnimator;

    private bool isPressed = false;

    private bool canInteract = false;

    private void Update()
    {
        if( canInteract && !isPressed && Input.GetKeyDown(KeyCode.E))
        {
            PressButton();
        }
    }
    void PressButton()
    {
        Debug.Log("��ư�� ���Ƚ��ϴ�.");
        isPressed = true;

        if (buttonAnimator != null)
        {
            buttonAnimator.SetTrigger("Press");
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("�÷��̾ ���Դϴ�.");
            player.SetActive(false); //���� �Ŵ����� �´� �Լ��� ����
        }
        else
        {
            Debug.LogWarning("�÷��̾� �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            Debug.Log("�÷��̾ ��ư ������ ���Խ��ϴ�.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            Debug.Log("�÷��̾ ��ư �������� �������ϴ�.");
        }
    }


}
