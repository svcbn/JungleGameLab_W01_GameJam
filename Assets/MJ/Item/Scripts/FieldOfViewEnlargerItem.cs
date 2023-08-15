using UnityEngine;

/// <summary>
/// [MJ] 시야를 더 넓혀주는 아이템에 적용될 스크립트
/// </summary>
public class FieldOfViewEnlargerItem : PlayerItem
{
    [Tooltip("증가시킬 배수를 입력 (기존 나침반 속도 * value)")]
    public int value;
    protected override void Execute(PlayerController target)
    {
        var cameraObj = GameObject.Find("MainCamera");
        cameraObj.transform.position -= new Vector3(0, 0, value);
    }
}