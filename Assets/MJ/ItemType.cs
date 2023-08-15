using System.ComponentModel;

/// <summary>
/// [MJ] Description 속성에 실제 사용 중인 해당 프리팹의 이름이 설정 
/// </summary>
public enum ItemType
{
    Ignore = 0,
    // 플레이어 버프 아이템
    [Description("HpItem")]
    PlayerHpUp,
    [Description("BarrierItem")]
    PlayerBarrier,
    
    // 몬스터 덫 관련 아이템
    [Description("StunTrap")]
    EnemyStun,
    [Description("SlowTrap")]
    EnemySlow,
    [Description("MoveReserveTrap")]
    EnemyMoveReserve,
    
    // 유틸 프리팹 아이템
    [Description("ArrowSpinnerChangeDirectionItem")]
    CompassChangeDirection,
    [Description("ArrowSpinnerSpeedUpItem")]
    CompassArrowSpeedUp,
    [Description("ArrowSpinnerSizeUpItem")]
    CompassArrowSizeUp,
    [Description("ViewEnlargarItem")]
    CameraZoomOut,
}