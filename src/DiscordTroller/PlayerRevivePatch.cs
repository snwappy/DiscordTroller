using DiscordTroller;
using HarmonyLib;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[HarmonyPatch(typeof(Character), "RPCA_Revive")]
static class Patch_PlayerRevive
{
    static void Postfix(Character __instance)
    {
        //Retrieve player name
        string name = Player.localPlayer.photonView.Owner.NickName;

        // Retrieve local computer time for the timestamp
        string localDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // Retrieve game run time session
        float time = RunTimeHelper.GetRunTime();

        // Y axis height meters tracking from the game code
        float height = 0f;
        if (__instance.refs.stats != null)
        {
            height = __instance.refs.stats.heightInMeters;
        }

        // Webhook message constructor
        string msg = MessageFormatter.Format(Plugin.TplRevive.Value, new Dictionary<string, string>
        {
            ["player"] = name,
            ["datetime"] = localDateTime,
            ["runtime"] = RunTimeHelper.FormatTime(time),
            ["height"] = height.ToString("0.0"),
            ["biome"] = GameStateHelper.GetReadableState(),
            ["room"] = GameStateHelper.GetRoomSize(),
            ["mode"] = GameStateHelper.GetGameMode(),
        });

        // Send webhook
        DiscordWebhook.Send(msg);
    }
}
