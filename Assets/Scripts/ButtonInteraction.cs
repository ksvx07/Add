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
        Debug.Log("버튼이 눌렸습니다.");
        isPressed = true;

        if (buttonAnimator != null)
        {
            buttonAnimator.SetTrigger("Press");
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("플레이어를 죽입니다.");
            player.SetActive(false); //게임 매니저에 맞는 함수로 수정
        }
        else
        {
            Debug.LogWarning("플레이어 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            Debug.Log("플레이어가 버튼 범위에 들어왔습니다.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            Debug.Log("플레이어가 버튼 범위에서 나갔습니다.");
        }
    }


}
