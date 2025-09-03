using System;
using TMPro;
using UnityEngine;

public class UIMAnager : SingletonObject<UIMAnager>
{
    [SerializeField] private ShowStageNamePanel showStageNamePanel;
    [SerializeField] private TextMeshProUGUI resetCountText;
    public void StartStage(int stage)
    {
        ShowStageName(stage);
    }
    public void ShowStageName(int stage)
    {
        showStageNamePanel.ShowStageName(GameConstant.stageName[stage]);
    }
    public void UpdateResetCount()
    {
        resetCountText.text = "X " + GameManager.resetCount;
    }
}