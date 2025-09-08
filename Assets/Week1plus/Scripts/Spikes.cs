using UnityEngine;

public class Spikes : MonoBehaviour
{
    private float rayPos = 0.6f;
    private float rayMaxDis = 0.2f;

    private void Update()
    {
        Debug.DrawRay(transform.position - transform.up * rayPos, transform.up * rayMaxDis, Color.blue);


        if (Physics.Raycast(transform.position - transform.up * rayPos, transform.up, out RaycastHit hit, rayMaxDis))
        {
            if (hit.collider.CompareTag(GameConstant.playerTag))
            {
                GameManager2.instance.Die();
            }

            if (hit.collider.CompareTag("Pushable"))
            {
                DestroyPushableBox(hit.transform.parent.parent.gameObject);
            }
        }
    }

    private void DestroyPushableBox(GameObject box)
    {
        if (!GameManager2.instance.busy)
        {
            GameManager2.instance.DestroyBox(box);
            //Destroy(box);
        }
    }
}
