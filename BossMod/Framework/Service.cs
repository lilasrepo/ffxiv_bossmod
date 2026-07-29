using Dalamud.Bindings.ImGui;
using Dalamud.Interface;
using Dalamud.Interface.ManagedFontAtlas;
using Dalamud.Game;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using System.Collections.Concurrent;

namespace BossMod;

public sealed class Service
{
#pragma warning disable CS8618
    [PluginService] public static IPluginLog Logger { get; private set; }
    [PluginService] public static IChatGui ChatGui { get; private set; }
    [PluginService] public static IGameGui GameGui { get; private set; }
    [PluginService] public static IGameConfig GameConfig { get; private set; }
    [PluginService] public static IGameInteropProvider Hook { get; private set; }
    [PluginService] public static ISigScanner SigScanner { get; private set; }
    [PluginService] public static ICondition Condition { get; private set; }
    [PluginService] public static IFramework Framework { get; private set; }
    [PluginService] public static ITextureProvider Texture { get; private set; }
    [PluginService] public static ICommandManager CommandManager { get; private set; }
    [PluginService] public static IDtrBar DtrBar { get; private set; }
    [PluginService] public static IDalamudPluginInterface PluginInterface { get; private set; }
    // TODO: get rid of stuff below in favour of CS
    [PluginService] public static IClientState ClientState { get; private set; }
    [PluginService] public static IObjectTable ObjectTable { get; private set; }
    // [PluginService] public static IPlayerState PlayerState { get; private set; } // TODO(api12): API15-only service
    [PluginService] public static ITargetManager TargetManager { get; private set; }
    [PluginService] public static IKeyState KeyState { get; private set; }
    [PluginService] public static INotificationManager Notifications { get; private set; }
#pragma warning restore CS8618

#pragma warning disable CA2211
    public static Action<string>? LogHandlerDebug;
    public static Action<string>? LogHandlerVerbose;
    public static void Log(string msg) => LogHandlerDebug?.Invoke(msg);
    public static void LogVerbose(string msg) => LogHandlerVerbose?.Invoke(msg);

    public static bool IsDev = true;

    public static void ChatMessage(string msg) => ChatGui.Print(msg, "VBM");
    public static void ChatError(string msg) => ChatGui.PrintError(msg, "VBM");

    public static Lumina.GameData LuminaGameData = null!;
    public static Lumina.Excel.ExcelSheet<T>? LuminaSheet<T>() where T : struct, Lumina.Excel.IExcelRow<T> => LuminaGameData.GetExcelSheet<T>();
    public static T? LuminaRow<T>(uint row) where T : struct, Lumina.Excel.IExcelRow<T> => LuminaSheet<T>()?.GetRowOrDefault(row);
    public static ConcurrentDictionary<Lumina.Text.ReadOnly.ReadOnlySeString, Lumina.Text.ReadOnly.ReadOnlySeString> LuminaRSV = []; // TODO: reconsider

    // Upstream types this as DalaMock's IWindowSystem so its test harness can substitute it.
    // DalaMock publishes net10 assets only and is not referenced here, so the CONCRETE Dalamud
    // class is used -- and it must stay nullable-concrete because Plugin.cs assigns
    // `Service.WindowSystem = new("vbm")`, a target-typed new.
    public static WindowSystem? WindowSystem;
    // Kept only so UIDev/ still compiles; UIDev is not part of the plugin build.
    public static ImFontPtr IconFontDev = default;
#pragma warning restore CA2211

    // Upstream assigns these inside TickService (Service.IconFont = uiBuilder.FontIcon, etc.), which
    // this tree drops. Computed, not captured: a ctor-time capture would latch a font before the
    // atlas is built and would go stale when Dalamud rebuilds it on a scale/DPI change.
    // Consumers: MiniArena/UIMisc (IconFont), ConfigChangelog (MonoFont), GaugeVisualizer (FontAtlas).
    // Upstream's Service.FileDialogManager is deliberately absent -- with TickService gone nothing
    // reads it (TC's pinned ReplayManager.cs uses its own path).
    public static ImFontPtr IconFont => UiBuilder.IconFont;
    public static ImFontPtr MonoFont => UiBuilder.MonoFont;
    public static IFontAtlas FontAtlas => PluginInterface.UiBuilder.FontAtlas;


    public static bool IsUIDev => PluginInterface == null;
    public static bool IsMock;   // upstream's DalaMock harness flag; always false here

    public static readonly ConfigRoot Config = new();

    //public static SharpDX.Direct3D11.Device? Device = null;
}
