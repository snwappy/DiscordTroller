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

        // Retrieve game run time session
        float time = RunTimeHelper.GetRunTime();

        // Retrieve local computer time for the timestamp
        string localDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //Retrieve player name
        string name = Player.localPlayer.photonView.Owner.NickName;

        // Y axis height meters tracking from the game code
        // this doesn't work at Peak?

        //float height = 0f;
        //if (__instance.refs.stats != null)
        //{
        //    height = __instance.refs.stats.heightInMeters;
        //}

        // Webhook message constructor
        string msg = MessageFormatter.Format(Plugin.TplPeak.Value, new Dictionary<string, string>
        {
            ["player"] = name,
            ["datetime"] = localDateTime,
            ["runtime"] = RunTimeHelper.FormatTime(time),
            ["height"] = "So peaked.",
            ["biome"] = GameStateHelper.GetReadableState(),
            ["room"] = GameStateHelper.GetRoomSize(),
            ["mode"] = GameStateHelper.GetGameMode(),
        });

        // Send webhook
        DiscordWebhook.Send(msg);

    }
}
