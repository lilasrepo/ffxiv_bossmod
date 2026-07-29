using System.IO;

namespace BossMod;

// [TC] Minimal storage-dir helper. Upstream's full ReplayHistory.cs also declares
// `record struct ReplayMemory` (identical to the one TC's pinned ReplayManagementConfig.cs owns
// -> CS0101 duplicate) plus Load/Save via Service.PluginLog. TC keeps only GetStorageDir(), the
// single ReplayHistory member referenced in the tree (ObstacleMapManager). Pinned via
// .walkback-paths so the upstream full version can't reintroduce the duplicate on a refresh.
public class ReplayHistory
{
    public static DirectoryInfo GetStorageDir()
    {
        var dir = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "vbm"));
        if (!dir.Exists)
            dir.Create();

        return dir;
    }
}
