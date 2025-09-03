using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum BlockType
{
    None,
    Disappear,
    MoveLeft,
    MoveRight
}

public class BlockController : MonoBehaviour
{
    [SerializeField] private List<BlockType> blockTypeByStage = new();
    [SerializeField] private GameObject disappearTrigger;
    [SerializeField] private GameObject moveTrigger;
    private BlockType blockType => blockTypeByStage[GameManager.stage];
    private Vector3 startPos;
    private float moveDistance = 3.0f;
    private bool hasTriggerWork;

    void Awake()
    {
        GameManager.Instance.OnStageStart += Initialize;
        GameManager.Instance.OnStageRestart += Initialize;
        startPos = transform.position;
    }

    public void Initialize()
    {
        transform.position = startPos;
        disappearTrigger.SetActive(blockType == BlockType.Disappear);
        moveTrigger.SetActive(blockType == BlockType.MoveLeft || blockType == BlockType.MoveRight);
        gameObject.SetActive(true);
        hasTriggerWork = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.playerTag) == false) return;
        if (hasTriggerWork == true) return;

        Disappear();
        Move();
    }

    public void Disappear()
    {
        if (blockType != BlockType.Disappear) return;

        hasTriggerWork = true;
        gameObject.SetActive(false);
    }

    public void Move()
    {
        if (blockType == BlockType.None || blockType == BlockType.Disappear) return;

        hasTriggerWork = true;
        float dis = blockType == BlockType.MoveLeft ? moveDistance : -moveDistance;
        transform.DOMove(transform.position + Vector3.right * dis, 0.5f).SetEase(Ease.OutQuad);
    }
}