using DiscordTroller;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[HarmonyPatch(typeof(Character), "RPCA_Revive")]
static class Patch_PlayerRevive
{
    static void Postfix(Character __instance)
    {
        string name = Player.localPlayer.photonView.Owner.NickName;

        string localDateTime =
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        float time = RunTimeHelper.GetRunTime();

        float height = 0f;
        if (__instance.refs.stats != null)
        {
            height = __instance.refs.stats.heightInMeters;
        }

        string msg = MessageFormatter.Format(Plugin.TplRevive.Value, new Dictionary<string, string>
        {
            ["player"] = name,
            ["datetime"] = localDateTime,
            ["runtime"] = RunTimeHelper.FormatTime(time),
            ["height"] = height.ToString("0.0"),
            ["biome"] = GameStateHelper.GetReadableState(),
            ["room"] = GameStateHelper.GetRoomSize(),
        });
        DiscordWebhook.Send(msg);
    }
}
