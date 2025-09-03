using System.Collections.Generic;
using TMPro;
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
    private Animator _animator => GetComponent<Animator>();
    private Vector3 startPos;
    private float moveDistance = 2.0f;

    void Awake()
    {
        GameManager.Instance.OnStageRestart += Initialize;
        startPos = transform.position;
    }

    public void Initialize()
    {
        transform.position = startPos;
        gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false) return;
        if (blockTypeByStage[GameManager.stage] == BlockType.Disappear) gameObject.SetActive(false);
    }

    public void Move()
    {
        float dis = blockTypeByStage[GameManager.stage] == BlockType.MoveLeft ? moveDistance : -moveDistance;
        transform.DOMove(transform.position + Vector3.right * dis, 0.5f).SetEase(Ease.OutQuad);
    }
}