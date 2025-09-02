using System;
using UnityEngine;

public class Mingku87_GameManager : Mingku87_SingletonObject<Mingku87_GameManager>
{
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
    }

    private void LockCursor(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }
}