namespace Data.Core
{
    public enum EndCondition
    {
        OnAllBlocksCleared,
        OnTargetBlocksCleared,
        OnTimerExpired,
        OnMoveCountReached,
        Manual
    }
}