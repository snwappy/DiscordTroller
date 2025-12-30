using UnityEngine;

static class RunTimeHelper
{
    public static float GetRunTime()
    {
        RunManager rm = Object.FindObjectOfType<RunManager>();
        if (rm == null) return -1f;
        return rm.timeSinceRunStarted;
    }

    public static string FormatTime(float seconds)
    {
        if (seconds < 0f) return "Unknown";

        int total = (int)seconds;
        int hours = total / 3600;
        int minutes = (total % 3600) / 60;
        int secs = total % 60;

        return
            hours.ToString("00") + ":" +
            minutes.ToString("00") + ":" +
            secs.ToString("00");
    }
}
