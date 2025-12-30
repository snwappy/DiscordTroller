using DiscordTroller;
using HarmonyLib;

[HarmonyPatch(typeof(RichPresenceService), "SetState")]
static class Patch_RichPresenceService_SetState
{
    static void Prefix(RichPresenceState state)
    {
        GameStateCache.Set(state);
        //Outputting this log to BepInEx
        Plugin.Log.LogInfo("[GameState] Changed to: " + state);
    }
}
