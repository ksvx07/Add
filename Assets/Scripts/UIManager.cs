using System;
using TMPro;
using UnityEngine;

public class UIMAnager : SingletonObject<UIMAnager>
{
    [SerializeField] private ShowStageNamePanel showStageNamePanel;
    [SerializeField] private TextMeshProUGUI resetCountText;
    public static int resetCount;

    protected override void Awake()
    {
        base.Awake();
        resetCount = 0;
        GameManager.Instance.OnGameRestart += UpdateResetCount;
    }

    public void StartStage(int stage)
    {
        showStageNamePanel.ShowStageName(GameConstant.stageName[stage]);
    }

    public void UpdateResetCount()
    {
        resetCount++;
        resetCountText.text = "X " + resetCount;
    }
}