using DiscordTroller;
using HarmonyLib;
using System;
using System.Collections.Generic;

[HarmonyPatch(typeof(RunManager), "StartRun")]
class Patch_RunStart_ResetDeath
{
    static bool sent = false;

    static void Postfix(Character __instance)
    {
        sent = true;

        Patch_PlayerDeath.Reset();

        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        string playerName = LogUtil.PlayerName();

        float height = 0f;
        if (__instance.refs.stats != null)
        {
            height = __instance.refs.stats.heightInMeters;
        }

        string msg = MessageFormatter.Format(Plugin.TplRunStart.Value, new Dictionary<string, string>
        {
            ["player"] = playerName,
            ["datetime"] = time,
            ["runtime"] = RunTimeHelper.FormatTime(RunTimeHelper.GetRunTime()),
            ["height"] = height.ToString("0.0"),
            ["biome"] = GameStateHelper.GetReadableState(),
            ["room"] = GameStateHelper.GetRoomSize(),
        });
        DiscordWebhook.Send(msg);
    }

    [HarmonyPatch(typeof(RunManager), "EndRun")]
    class Patch_RunEnd_ResetFlag
    {
        static void Prefix()
        {
            sent = false;
        }
    }
}
