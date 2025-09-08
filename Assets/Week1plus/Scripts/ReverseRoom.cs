using UnityEngine;

public class ReverseRoom : MonoBehaviour
{
    public Transform player;
    private Animator animator;

    public Transform pushable1;
    public Transform pushable2;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager2.instance.thisLevel < 4) return;
        if(Input.GetKeyDown(KeyCode.J)) animator.SetTrigger("Reverse");
        if (!GameManager2.instance.busy)
        {
            if (GameManager2.instance.keyQ) Reverse();
        }
    }

    void Reverse()
    {
        GameManager2.instance.StartAction();
        GameManager2.instance.EnableGravity(false);
        player.SetParent(transform, true);

        animator.ResetTrigger("Reset");
        animator.SetTrigger("Reverse");
    }

    public void ReverseEnd()
    {
        player.SetParent(null, true);
        player.rotation = Quaternion.Euler(Vector3.zero);
        if (pushable1 != null)
        {
            pushable1.Rotate(new Vector3(0, 0, 180));
        }
        if (pushable2 != null)
        {
            pushable2.Rotate(new Vector3(0, 0, 180));
        }

        GameManager2.instance.EnableGravity(true);
        GameManager2.instance.EndAction();
    }


    public void ResetRoom()
    {
        Debug.Log("ResetRoom");
        animator.SetTrigger("Reset");
    }
}
