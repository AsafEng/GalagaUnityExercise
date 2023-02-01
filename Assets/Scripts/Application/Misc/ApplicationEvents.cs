
// This class will give static access to the game events strings.
public class ApplicationEvents
{
    //Input events
    public const string PressedRight = "PressedRight";
    public const string PressedLeft = "PressedLeft";
    public const string PressedMovementKey = "PressedMovementKey";
    public const string PressedSpace = "PressedSpace";
    public const string ReleasedLeftOrRight = "ReleasedLeftOrRight";

    //Player related events
    public const string PlayerShooting = "PlayerShooting";
    public const string PlayerHit = "PlayerHit";
    public const string PlayerReset = "PlayerReset";
    public const string EnemyCollidedPlayer = "EnemyCollidedPlayer";
    public const string GainedScore = "GainedScore";
    public const string CollectedPowerup = "CollectedPowerup";

    //Game scene state events
    public const string FinishedGame = "WonGame";
    public const string ExitingGame = "ExitingGame";
    public const string StartingGame = "StartingGame";
    public const string RestartingGame = "RestartingGame";
    public const string StartingLevel = "StartingLevel";

    //Enemy spawn events
    public const string SpawningEnemy = "SpawningEnemy";
    public const string EnemyKilled = "EnemyKilled";
    public const string FilledGrid = "FilledGrid";

    //Score related events
    public const string PressedScoreboard = "PressedScoreboard";
    public const string SavingNewScore = "SavingNewScore";
    public const string UpdatedPlayerName = "UpdatedPlayerName";
}