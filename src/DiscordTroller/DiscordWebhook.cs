using DiscordTroller;
using System.Net.Http;
using System.Text;

static class DiscordWebhook
{
    static readonly HttpClient client = new HttpClient
    {
        Timeout = System.TimeSpan.FromSeconds(5)
    };

    public static async void Send(string message)
    {
        try
        {
            if (!Plugin.WebhookEnabled.Value)
                return;

            string webhookUrl = Plugin.WebhookUrl.Value;
            string threadId = Plugin.ThreadId.Value;

            if (string.IsNullOrWhiteSpace(webhookUrl))
            {
                Plugin.Log.LogWarning(
                    "[DiscordTroller/DiscordWebhook] WebhookUrl = null. Sending message skipped..."
                );
                return;
            }

            string safeMessage = EscapeJson(message);
            string json = "{\"content\":\"" + safeMessage + "\"}";

            StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");

            string url = webhookUrl;

            if (!string.IsNullOrWhiteSpace(threadId))
                url += "?thread_id=" + threadId;

            HttpResponseMessage response =
                await client.PostAsync(url, content);

            Plugin.Log.LogInfo(
                "[DiscordTroller/DiscordWebhook] Response: " +
                ((int)response.StatusCode) + " " + response.StatusCode
            );
        }
        catch (System.Exception ex)
        {
            Plugin.Log.LogError("[DiscordTroller/DiscordWebhook] Unexpected error sending webhook...");
            Plugin.Log.LogError(ex.ToString());
        }
    }

    static string EscapeJson(string text)
    {
        if (string.IsNullOrEmpty(text))
            return "";

        return text
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\n", "\\n")
            .Replace("\r", "");
    }
}
