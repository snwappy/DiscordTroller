using Photon.Pun;

static class GameStateHelper
{
    public static RichPresenceState GetState()
    {
        return GameStateCache.CurrentState;
    }

    public static string GetReadableState()
    {
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
    {
        if (PhotonNetwork.OfflineMode)
            return "Offline / Single-Player";

        if (!PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom == null)
            return "Null";

        return $"Online / Multiplayer: {PhotonNetwork.PlayerList.Length}/{PhotonNetwork.CurrentRoom.MaxPlayers}";
    }
}
