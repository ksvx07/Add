using System.Collections.Generic;

public static class GameConstant
{
    public static string playerTag = "Player";
    public static string groundTag = "Ground";

    public const float playerSpeed = 8f;
    public const int maxJumpCount = 1;
    public const float playerJumpForce = 10f;
    public const float playerGravityMultiplier = 2f;
    public const float playerFallMultiplier = 4f;

    public const float textTypingSpeed = 0.05f;

    public const string fakeTrigger = "Fake";
    public const string resetTrigger = "Reset";

    public static List<string> stageName = new() { "", "Run", "rrr" };

    public const int playerJumpLimitStage = 3;
    public const int playerJumpLimitCount = 10;
    public const int playerDieWhenStopStage = 5;
    public const float playerDieTimer = 0.3f;
}