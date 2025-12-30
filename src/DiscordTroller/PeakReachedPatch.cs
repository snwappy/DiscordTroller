using DiscordTroller;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[HarmonyPatch(typeof(PeakHandler), "EndCutscene")]
class Patch_PeakReached
{
    static bool sent = false;

    static void Prefix(Character __instance)
    {
        if (sent) return;
        sent = true;

        float time = RunTimeHelper.GetRunTime();
        string formatted = RunTimeHelper.FormatTime(time);

        DateTime now = DateTime.Now;
        string localDateTime = now.ToString("yyyy-MM-dd HH:mm:ss");

        string name = LogUtil.PlayerName();

        float height = 0f;
        if (__instance.refs.stats != null)
        {
            height = __instance.refs.stats.heightInMeters;
        }

        string msg = MessageFormatter.Format(Plugin.TplPeak.Value, new Dictionary<string, string>
        {
            ["player"] = name,
            ["datetime"] = localDateTime,
            ["runtime"] = formatted,
            ["biome"] = GameStateHelper.GetReadableState(),
            ["room"] = GameStateHelper.GetRoomSize(),
        });
        DiscordWebhook.Send(msg);

    }
}
