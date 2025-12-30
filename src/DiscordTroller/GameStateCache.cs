static class GameStateCache
{
    public static RichPresenceState CurrentState { get; private set; }
        = RichPresenceState.Status_MainMenu;

    public static void Set(RichPresenceState state)
    {
        CurrentState = state;
    }
}
