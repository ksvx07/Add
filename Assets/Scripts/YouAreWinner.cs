using Unity.VisualScripting;
using UnityEngine;

public class YouAreWinner : MonoBehaviour
{
    public GameObject wintext;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wintext.SetActive(true);
        }
    }
}
