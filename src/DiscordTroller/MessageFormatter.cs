using System.Collections.Generic;

static class MessageFormatter
{
    public static string Format(string template, Dictionary<string, string> vars)
    {
        if (string.IsNullOrEmpty(template)) return "";
        string s = template.Replace("\\n", "\n");

        foreach (var kv in vars)
            s = s.Replace("{" + kv.Key + "}", kv.Value ?? "");

        return s;
    }
}
