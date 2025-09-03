using System;
using UnityEngine;

public class GameManager : SingletonObject<GameManager>
{
    public static int stage;
    public static bool isPlaying = false;
    public static bool isClear = false;
    public static bool canMove = true;
    public event Action OnGameRestart;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        LockCursor(true);
        stage = 1;
    }

    private void LockCursor(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    public void ClearStage()
    {
        stage++;
        if (stage == 10) ClearGame();
    }

    public void StartStage()
    {
        UIMAnager.Instance.StartStage(stage);
    }

    public void ClearGame()
    {

    }

    public void Restart()
    {
        OnGameRestart?.Invoke();
    }
}