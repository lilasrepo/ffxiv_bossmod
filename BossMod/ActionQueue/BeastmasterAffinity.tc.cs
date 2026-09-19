namespace BossMod;

// TODO(api13): FFXIVClientStructs 6966 (TC game 7.20) predates Beastmaster (7.5), so the CS enum this mirrors
// does not exist. Kept so ActionDefinitions.TrickAffinity keeps upstream's shape; its only consumer, the BST
// rotation module (BossMod.Autorotation/Standard/xan/Melee/BST.cs), is Compile-Removed on TC.
public enum BeastmasterAffinity : byte
{
    None,
    Rampant,
    Durant,
    Eldritch,
    Volant,
}
