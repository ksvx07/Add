using TMPro;
using UnityEngine;
using System.Threading.Tasks;

public class UIMAnager : SingletonObject<UIMAnager>
{
    [SerializeField] private ShowStageNamePanel showStageNamePanel;
    [SerializeField] private TextMeshProUGUI resetCountText;
    [SerializeField] private GameObject fadeOut;
    [SerializeField] private GameObject openDoorUI;
    private Animator fadeOutAnimator => fadeOut.GetComponent<Animator>();
    public static bool isFadeOutWorking = false;
    public static int resetCount;
    public static int fadeOutTime;


    protected override void Awake()
    {
        base.Awake();
        resetCount = 0;
        GameManager.Instance.OnStageStart += StartStage;
        GameManager.Instance.OnStageRestart += Reset;
        foreach (AnimationClip clip in fadeOutAnimator.runtimeAnimatorController.animationClips)
            if (clip.name == "FadeOut") fadeOutTime = (int)(clip.length / fadeOutAnimator.speed * 1000);
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
        fadeOutAnimator.SetTrigger("FadeOut");

        await Task.Delay(fadeOutTime);
        isFadeOutWorking = false;
    }

    async public void OpenDoor()
    {
        openDoorUI.SetActive(true);
        await Task.Delay(1500);
        openDoorUI.SetActive(false);
    }
}