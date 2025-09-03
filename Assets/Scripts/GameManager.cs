using System;
using UnityEngine;

public class GameManager : SingletonObject<GameManager>
{
    public static int stage;
    public static bool isPlaying = false;
    public static bool isClear = false;
    private float playTime;
    public static bool canMove = true;
    public event Action OnGameReset;
    public event Action OnGameRestart;
    private int resetCount;

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
}