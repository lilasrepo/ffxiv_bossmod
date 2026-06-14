using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.Group;
using System.Runtime.InteropServices;

namespace BossMod;

internal static class ClientStructsEx
{
    public static bool IsValidAllianceMember(this PartyMember member) => (member.Flags & 1) != 0;
}

// TODO i might have adjusted the wrong offset for the 7.3 fix but it doesn't really matter if all we care about is interpolation
[StructLayout(LayoutKind.Explicit, Size = 0x22E0)]
internal unsafe partial struct PlayerMove
{
    // this was 0x1E0 in 7.25
    [FieldOffset(0x1E0)] public MoveContainer Move;
}

[StructLayout(LayoutKind.Explicit, Size = 0x430)]
internal unsafe partial struct MoveContainer
{
    [StructLayout(LayoutKind.Explicit, Size = 0x88)]
    public unsafe partial struct InterpolationState
    {
        [FieldOffset(0x10)] public float DesiredRotation;
        [FieldOffset(0x14)] public float OriginalRotation;
        [FieldOffset(0x40)] public bool RotationInterpolationInProgress;
    }

    // this was 0x1C0 in 7.25
    [FieldOffset(0x1D0)] public InterpolationState Interpolation;
}

[StructLayout(LayoutKind.Explicit, Size = 0x76F0)]
internal unsafe partial struct ControlEx
{
    // porting-note(category-C, game-7.1): upstream HEAD reads BaseMoveSpeed at 0x7118 (game-7.5 Control layout).
    // On TC game 7.1 the field is at 0x7108 (the 7.5 struct grew by 0x10 before this offset). Reading 0x7118 on 7.1
    // returns a wrong field (≈0) -> ClientState.MoveSpeed becomes 0 -> NormalMovement passes gMultiplier = 1/0 = +Inf
    // to ThetaStar -> every path's PathLeeway goes negative -> pathfinder scores all goal cells worse than start and
    // returns no destination ("won't approach" + jitter under AutoDuty AI:off). 0x7108 is TC_ok's empirically-correct
    // game-7.1 offset (the operational source of truth, currently running on the TC client). Runtime-verified 2026-06-14.
    [FieldOffset(0x7108)] public float BaseMoveSpeed;

    public static ControlEx* Instance() => (ControlEx*)Control.Instance();
}
