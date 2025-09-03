using System.Xml.Serialization;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    public Animator doorAnimator;

    private bool hasTrigged = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasTrigged)
        {
            Debug.Log("플레이어가 문 열림 영역에 들어왔습니다.");

            doorAnimator.SetTrigger("Open");

            hasTrigged = true;
        }
    }
}
