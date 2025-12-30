using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace DiscordTroller;

[BepInAutoPlugin]
public partial class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log { get; private set; } = null!;

    internal static ConfigEntry<string> WebhookUrl = null!;
    internal static ConfigEntry<string> ThreadId = null!;
    internal static ConfigEntry<bool> WebhookEnabled = null!;

    internal static ConfigEntry<string> TplDeath = null!;
    internal static ConfigEntry<string> TplRevive = null!;
    internal static ConfigEntry<string> TplRunStart = null!;
    internal static ConfigEntry<string> TplPeak = null!;

    private readonly Harmony _harmony = new(Id);

    private void Awake()
    {
        Log = Logger;

        //TO-DO: add more console logging to BepInEx for further debugging purpose.

        WebhookEnabled = Config.Bind(
            "Discord",
            "Enabled",
            true,
            "Enable or disable Discord webhook messages"
        );

        WebhookUrl = Config.Bind(
            "Discord",
            "WebhookUrl",
            "",
            "Discord webhook URL"
        );

        ThreadId = Config.Bind(
            "Discord",
            "ThreadId",
            "",
            "Optional Discord thread ID (leave empty if unused)"
        );

        TplDeath = Config.Bind("Messages", "Death",
            "**{player}** died\nHeight: **{height} m**\nTimestamp: **{datetime}**\nRun session time: **{runtime}**",
            "Death message. Use \\n for new lines.");

        TplRevive = Config.Bind("Messages", "Revive",
            "**{player}** revived!\nTimestamp: **{datetime}**\nRun session time: **{runtime}**",
            "Player revive message (this currently does not work with Scout Effigy. Use \\n for new lines.");

        TplRunStart = Config.Bind("Messages", "RunStart",
            "▶️ **{player}** stared a new game session!\nTimestamp: **{datetime}**",
            "First game start announcement message. Use \\n for new lines.");

        TplPeak = Config.Bind("Messages", "Peak",
            "**{player}** ĐÃ PEAKED!\nTimestamp: **{datetime}**\nRun session time: **{runtime}**",
            "Peak reached message. Use \\n for new lines.");

        _harmony.PatchAll();
        Log.LogInfo($"Plugin {Name} is loaded!");
    }
}
