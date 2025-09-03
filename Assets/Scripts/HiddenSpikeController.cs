using UnityEngine;

public class HiddenSpikeController : MonoBehaviour
{
    [SerializeField] private GameObject spikes;
    void Awake()
    {
        GameManager.Instance.OnStageStart += Initialize;
        spikes.SetActive(false);
    }

    public void Initialize()
    {
        if (GameManager.stage < GameConstant.hiddenSpikeStage) return;
        spikes.SetActive(true);
    }
}
