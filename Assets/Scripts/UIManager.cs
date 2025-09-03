using TMPro;
using UnityEngine;
using System.Threading.Tasks;

public class UIMAnager : SingletonObject<UIMAnager>
{
    [SerializeField] private ShowStageNamePanel showStageNamePanel;
    [SerializeField] private TextMeshProUGUI resetCountText;
    [SerializeField] private GameObject fadeOut;
    public static bool isFadeOutWorking = false;
    public static int resetCount;

    protected override void Awake()
    {
        base.Awake();
        resetCount = 0;
        GameManager.Instance.OnStageStart += StartStage;
        GameManager.Instance.OnStageRestart += Reset;
    }

    public void StartStage()
    {
        showStageNamePanel.ShowStageName(GameConstant.stageName[GameManager.stage]);
    }

    public void Reset()
    {
        resetCount++;
        resetCountText.text = "X " + resetCount;
    }

    async public void FadeOut()
    {
        if (isFadeOutWorking) return;

        isFadeOutWorking = true;
        fadeOut.GetComponent<Animator>().SetTrigger("FadeOut");

        await Task.Delay(1500);
        isFadeOutWorking = false;
    }
}