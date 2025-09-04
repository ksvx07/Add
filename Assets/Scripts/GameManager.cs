using System;
using UnityEngine;
using System.Threading.Tasks;

public class GameManager : SingletonObject<GameManager>
{
    public static int stage;
    public static bool isPlaying = false;
    public static bool isClear = false;
    public static bool canMove = true;
    public event Action OnStageStart;
    public event Action OnStageRestart;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        LockCursor(true);
        stage = 0;
    }

    private void LockCursor(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    public void ClearStage()
    {
        stage++;
        DoorOpenTrigger.hasTrigged = false;
        if (stage >= GameConstant.stageName.Count) ClearGame();
    }

    async public void StartStage()
    {
        if (isClear) return;

        canMove = false;
        OnStageStart?.Invoke();
        await Task.Delay(2000);
        canMove = true;
    }

    public void ClearGame()
    {
        isClear = true;
        Debug.Log("clear");
    }

    async public void Restart()
    {
        UIMAnager.Instance.FadeOut();
        canMove = false;
        await Task.Delay(UIMAnager.fadeOutTime / 2);
        OnStageRestart?.Invoke();
        await Task.Delay(UIMAnager.fadeOutTime / 2);
        canMove = true;
    }
}