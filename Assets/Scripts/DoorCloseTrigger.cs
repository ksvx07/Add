using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{

    public Animator doorAnimator;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            Debug.Log("�÷��̾ �� ���� ������ ���Խ��ϴ�.");

            doorAnimator.SetTrigger("Close");
            hasTriggered = true;
        }
    }

}
