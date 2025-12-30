using DiscordTroller;
using HarmonyLib;
using System.Collections.Generic;
using System;
using UnityEngine;

[HarmonyPatch(typeof(Character), "RPCA_Die")]
class Patch_PlayerDeath
{
    static HashSet<Character> reported = new HashSet<Character>();

    public static void Reset()
    {
        reported.Clear();
    }

    static void Postfix(Character __instance)
    {
        float time = RunTimeHelper.GetRunTime();
        string formatted = RunTimeHelper.FormatTime(time);

        DateTime now = DateTime.Now;
        string localDateTime = now.ToString("yyyy-MM-dd HH:mm:ss");

        string name = Player.localPlayer.photonView.Owner.NickName;

        float height = 0f;
        if (__instance.refs.stats != null)
        {
            height = __instance.refs.stats.heightInMeters;
        }

        string msg = MessageFormatter.Format(Plugin.TplDeath.Value, new Dictionary<string, string>
        {
            ["player"] = name,
            ["datetime"] = localDateTime,
            ["runtime"] = formatted,
            ["height"] = height.ToString("0.0"),
            ["biome"] = GameStateHelper.GetReadableState(),
            ["room"] = GameStateHelper.GetRoomSize(),
        });
        DiscordWebhook.Send(msg);
    }
}
