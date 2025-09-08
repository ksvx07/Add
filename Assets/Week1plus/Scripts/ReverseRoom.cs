using UnityEngine;

public class ReverseRoom : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GameManager2.instance.busy)
        {
            if (Input.GetKeyDown(KeyCode.Q)) Reverse();
        }
    }

    void Reverse()
    {
        GameManager2.instance.StartAction();
        GameManager2.instance.EnableGravity(false);
        player.SetParent(transform, true);

        animator.SetTrigger("Reverse");
    }

    public void ReverseEnd()
    {
        player.SetParent(null, true);
        player.rotation = Quaternion.Euler(Vector3.zero);
        GameManager2.instance.EnableGravity(true);
        GameManager2.instance.EndAction();
    }
}
