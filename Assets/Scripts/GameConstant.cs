using System.Collections.Generic;

public static class GameConstant
{
    public static string playerTag = "Player";
    public static string groundTag = "Ground";

    public const float playerSpeed = 6f;
    public const int maxJumpCount = 1;
    public const float playerJumpForce = 8f;
    public const float playerGravityMultiplier = 2f;
    public const float playerFallMultiplier = 4f;

    public const float textTypingSpeed = 0.05f;

    public const string fakeTrigger = "Fake";
    public const string resetTrigger = "Reset";

    public static List<string> stageName = new() {
    "문어 게임",
    "보안 시스템 가동",
    "띵동",
    "느려",
    "안 움직이면 쏜다",
    "깨시조아",
    "층간소음",
    "반전술식",};

    public const int startSpikeTrapStage = 1;
    public const int playerJumpLimitStage = 2;
    public const int playerJumpLimitCount = 10;
    public const int playerDieWhenStopStage = 4;
    public const float playerDieTimer = 0.3f;
    public const int hiddenSpikeStage = 5;
    public const int playerMoveFlipStage = 7;
}