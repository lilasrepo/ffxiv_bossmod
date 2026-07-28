# Boss Mod（繁中移植版 · TC13） / Traditional-Chinese Port

> 別再倒在王的機制上。<br>
> Don't fail at raid mechanics anymore.

**繁體中文**：這是 **[Boss Mod](https://github.com/awgil/ffxiv_bossmod)** 的繁體中文客戶端移植版，對應 **FFXIV 7.20 / yanmucorp Dalamud API13（.NET 9）**。本專案僅做相容性移植，**非官方、非原作維護**；所有原始功能與設計著作權歸原作者 **veyn**。

**English**: A Traditional-Chinese-client port of **[Boss Mod](https://github.com/awgil/ffxiv_bossmod)** targeting **FFXIV 7.20 / yanmucorp Dalamud API13 (.NET 9)**. Compatibility port only — **unofficial and not maintained by the original author**. All original work © **veyn**.

---

## 這是什麼 / About

一套協助應付王戰機制的工具，提供機制提示、走位輔助與自動循環（AutoRotation），降低高難度戰鬥的門檻。也是 AutoDuty 等自動化插件的戰鬥／循環基礎之一。

A toolkit that simplifies boss fights — mechanic hints, movement assistance and auto-rotation — lowering the barrier for high-end content. Also serves as a combat/rotation backend for automation plugins like AutoDuty.

## 安裝 / Installation

**繁體中文**
1. 使用 **XIVTCLauncher** 啟動繁體中文客戶端。
2. 遊戲內輸入 `/xlsettings` → 切到 **Experimental** 分頁 → **Custom Plugin Repositories（自訂插件庫）**。
3. 貼上下列網址並按 **+** 儲存：
   ```
   https://raw.githubusercontent.com/lilasrepo/DalamudPlugins/main/pluginmaster.json
   ```
4. 輸入 `/xlplugins`，搜尋 **Boss Mod (TC13)** → 安裝 → 啟用。

**English**
1. Launch the Traditional-Chinese client with **XIVTCLauncher**.
2. In-game, type `/xlsettings` → **Experimental** tab → **Custom Plugin Repositories**.
3. Add this URL and save with **+**:
   ```
   https://raw.githubusercontent.com/lilasrepo/DalamudPlugins/main/pluginmaster.json
   ```
4. Type `/xlplugins`, search **Boss Mod (TC13)** → Install → Enable.

## 對應版本 / Compatibility

| 項目 / Item | 版本 / Version |
|---|---|
| 遊戲 / Game | FFXIV 7.20（繁中客戶端 / TC client） |
| Dalamud | yanmucorp API13（.NET 9） |
| 移植自上游 / Ported from upstream | v7.5.0.26 |

## 原作與授權 / Credits & License

本專案 fork 自 **[awgil/ffxiv_bossmod](https://github.com/awgil/ffxiv_bossmod)**，授權沿用上游；所有原始功能著作權歸 **veyn**。<br>
Forked from **[awgil/ffxiv_bossmod](https://github.com/awgil/ffxiv_bossmod)**. License follows upstream; all original work © **veyn**.

## 免責聲明 / Disclaimer

第三方插件，使用風險自負。**移植相關問題請回報到本 repo 的 Issues，請勿打擾上游原作者。**<br>
Third-party plugin — use at your own risk. **For port-specific issues please open an Issue here; do not contact the upstream author.**
