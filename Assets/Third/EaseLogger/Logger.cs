using System;

namespace Ease
{
    /// <summary>
    /// 受条件编译控制
    /// 特定前缀
    /// </summary>
    public static class Logger
    {
        public static string PrefixLog { get; set; }
        public static string PrefixError { get; set; }
        public static string PrefixWarning { get; set; }

        public static event Action<string> eventLog;
        public static event Action<string> eventLogError;
        public static event Action<string> eventLogWarning;

        public static void Reset()
        {
            PrefixLog = "";
            PrefixError = "";
            PrefixWarning = "";
            eventLog = null;
            eventLogError = null;
            eventLogWarning = null;
        }

        public static void Log(string content)
        {
#if ENABLE_LOG
            eventLog?.Invoke(content);
#endif
        }

        public static void Log(object sender, params string[] contents)
        {
            Log(GetContent(PrefixLog, sender, contents));
        }

        public static void LogError(string content)
        {
            eventLogError?.Invoke(content);
        }

        public static void LogError(object sender, params string[] contents)
        {
            LogError(GetContent(PrefixError, sender, contents));
        }

        public static void LogWarning(string content)
        {
            eventLogWarning?.Invoke(content);
        }

        public static void LogWarning(object sender, params string[] contents)
        {
            LogWarning(GetContent(PrefixWarning, sender, contents));
        }

        private static string GetContent(string prefix, object sender, params string[] contents)
        {
            var content = "";
            if (!string.IsNullOrEmpty(prefix))
                content = $"[{prefix}]";
            if (sender != null)
                content = $"{content}:{sender.GetType().Name}";
            for (int i = 0; i < contents.Length; i++)
            {
                var item = contents[i];
                content = $"{content} {item}";
            }

            return content;
        }
    }
}