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
    // porting-note(category-C): 0x7118 is NOT a 7.5-only offset, as an earlier note here assumed. Upstream
    // introduced ControlEx with BaseMoveSpeed at 0x7118 in b87e9a041 (2025-08-07) -- the game-7.3 update wave,
    // the same date as every other 7.3 rescan we adopted this round (NoClippy a54d717, GatherBuddyReborn
    // aa8e2d83, Pictomancy 18056c6e, BossMod 4c1a83c6a) -- and it is unchanged at the last pre-7.4 revision
    // (2eb3b7132, 2025-12-15). The old 0x7108 was TC_ok's empirically-correct value for game 7.1, and the TC
    // client has since moved to the 7.3 executable (proven this round: the 7.3-era sigs resolve, the 7.1 ones
    // no longer do). FFXIVClientStructs 6966 is not a cross-check here -- its Control has no explicit field
    // offsets and does not name BaseMoveSpeed (Cecil-verified).
    // RUNTIME-VERIFY: reading the wrong field yields ≈0 -> ClientState.MoveSpeed = 0 -> NormalMovement passes
    // gMultiplier = 1/0 = +Inf to ThetaStar -> every path's PathLeeway goes negative -> the pathfinder returns
    // no destination ("won't approach" + jitter under AutoDuty AI:off). So the test is simply: run AutoDuty and
    // watch whether it walks. If it does not, revert this one number to 0x7108.
    [FieldOffset(0x7118)] public float BaseMoveSpeed;

    public static ControlEx* Instance() => (ControlEx*)Control.Instance();
}
