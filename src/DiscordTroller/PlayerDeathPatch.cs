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
        //Current local player instance only check
        if (__instance == null) return;

        if (__instance.refs == null ||
            __instance.refs.view == null ||
            !__instance.refs.view.IsMine)
            return;

        if (reported.Contains(__instance)) return;
        reported.Add(__instance);

        // Retrieve game run time session
        float time = RunTimeHelper.GetRunTime();

        // Retrieve local computer time for the timestamp
        string localDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //Retrieve player name
        string name = Player.localPlayer.photonView.Owner.NickName;

        // Y axis height meters tracking from the game code
        float height = 0f;
        if (__instance.refs.stats != null)
        {
            height = __instance.refs.stats.heightInMeters;
        }

        // Webhook message constructor
        string msg = MessageFormatter.Format(Plugin.TplDeath.Value, new Dictionary<string, string>
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
