using DiscordTroller;
using HarmonyLib;
using System;
using System.Collections.Generic;

[HarmonyPatch(typeof(RunManager), "StartRun")]
class Patch_RunStart_ResetDeath
{
    static bool sent = false;

    static void Postfix(RunManager __instance)
    {
        if (GameStateHelper.GetState() == RichPresenceState.Status_Airport)
        {
            Plugin.Log.LogInfo("Even though this is not exactly ideal and should have fixed in the code for the long-term future proof, message will not be sent out during airport.");
            return;
        }
        else
        {
            sent = true;
            // Reset the run time
            Patch_PlayerDeath.Reset();

            // Retrieve local computer time for the timestamp
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //Retrieve player name
            string playerName = Player.localPlayer.photonView.Owner.NickName;

            // Webhook message constructor
            string msg = MessageFormatter.Format(Plugin.TplRunStart.Value, new Dictionary<string, string>
            {
                ["player"] = playerName,
                ["datetime"] = time,
                ["runtime"] = RunTimeHelper.FormatTime(RunTimeHelper.GetRunTime()),
                ["biome"] = GameStateHelper.GetReadableState(),
                ["room"] = GameStateHelper.GetRoomSize(),
                ["mode"] = GameStateHelper.GetGameMode(),
            });

            // Send webhook
            DiscordWebhook.Send(msg);
        }

    }
}
