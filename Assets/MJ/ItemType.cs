using System.ComponentModel;

public enum ItemType
{
    Ignore = 0,
    [Description("")]
    PlayerHpUp,
    PlayerBarrier,
    
    CompassChangeDirection,
    CompassArrowSpeedUp,
    
    CompassArrowSizeUp,
    EnemyStun,
    EnemySlow,
    EnemyMoveReserve,
    
    CameraZoomOut,
}