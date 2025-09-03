using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{

    public Animator doorAnimator;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            Debug.Log("플레이어가 문 닫힘 영역에 들어왔습니다.");

            doorAnimator.SetTrigger("Close");
            hasTriggered = true;
        }
    }

}
