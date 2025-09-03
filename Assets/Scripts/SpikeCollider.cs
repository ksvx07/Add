using UnityEngine;

public class SpikeCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.playerTag) == false) return;
        PlayerController.Instance.Die();
    }
}