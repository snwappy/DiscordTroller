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
            Patch_PlayerDeath.Reset();
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string playerName = Player.localPlayer.photonView.Owner.NickName;
            string msg = MessageFormatter.Format(Plugin.TplRunStart.Value, new Dictionary<string, string>
            {
                ["player"] = playerName,
                ["datetime"] = time,
                ["runtime"] = RunTimeHelper.FormatTime(RunTimeHelper.GetRunTime()),
                ["biome"] = GameStateHelper.GetReadableState(),
                ["room"] = GameStateHelper.GetRoomSize(),
            });
            DiscordWebhook.Send(msg);
        }

    }
}
