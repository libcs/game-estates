using System;
using System.Threading.Tasks;

namespace Gamer.Core
{
    public class Debug
    {
        public static readonly string Platform;
        public static Action<bool> AssertFunc;
        public static Action<string> LogFunc;
        public static Action<string, object[]> LogFormatFunc;
        public static void Assert(bool condition) => AssertFunc(condition);
        public static void Log() { }
        public static void Log(string format) => LogFunc(format);
        public static void LogFormat(string format, params object[] args) => LogFormatFunc(format, args);

        static Debug()
        {
            var task = Task.Run(() => UnityEngine.Application.platform.ToString());
            try
            {
                Platform = task.Result;
                AssertFunc = x => UnityEngine.Debug.Assert(x);
                LogFunc = a => UnityEngine.Debug.Log(a);
                LogFormatFunc = (a, b) => UnityEngine.Debug.LogFormat(a, b);
                return;
            }
            catch
            {
                Platform = string.Empty;
                AssertFunc = x => System.Diagnostics.Debug.Assert(x);
                LogFunc = a => System.Diagnostics.Debug.Print(a);
                LogFormatFunc = (a, b) => System.Diagnostics.Debug.Print(a, b);
            }
        }
    }
}