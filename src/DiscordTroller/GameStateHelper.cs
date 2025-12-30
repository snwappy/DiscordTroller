using Photon.Pun;

static class GameStateHelper
{
    public static RichPresenceState GetState()
    {
        return GameStateCache.CurrentState;
    }

    public static string GetReadableState()
    {
        //Retreive information about the game location status
        //then caching it (for background update) with GameStateCache
        return GetState() switch
        {
            RichPresenceState.Status_MainMenu => "Main Menu",
            RichPresenceState.Status_Airport => "Airport",
            RichPresenceState.Status_Shore => "Shore",
            RichPresenceState.Status_Tropics => "Tropics",
            RichPresenceState.Status_Roots => "Roots",
            RichPresenceState.Status_Alpine => "Alpine",
            RichPresenceState.Status_Mesa => "Mesa",
            RichPresenceState.Status_Caldera => "Caldera",
            RichPresenceState.Status_Kiln => "Kiln",
            RichPresenceState.Status_Peak => "Peak",
            _ => "Unknown"
        };
    }

    public static string GetRoomSize()
        //Retrieve information about the game session if it is a multiplayer or singapore :)
    {
        if (PhotonNetwork.OfflineMode)
            return "Offline / Single-Player";

        if (!PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom == null)
            return "Null";

        return $"Online / Multiplayer: {PhotonNetwork.PlayerList.Length}/{PhotonNetwork.CurrentRoom.MaxPlayers}";
    }

    public static string GetGameMode()
    {
        //Track what game mode is being played, is it Ascend (1-7) [or even higher if using mods], or is it just Tenderfoot and Peak
        int a = Ascents.currentAscent;
        if (a == -1) return "Tenderfoot";
        if (a == 0) return "Peak";
        return $"Ascent {a}";
    }
}
