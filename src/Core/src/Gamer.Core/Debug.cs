using System;

namespace Gamer.Core
{
    public class Debug
    {
        public static Action<bool> AssertFunc = x => System.Diagnostics.Debug.Assert(x);
        public static Action<string> LogFunc = a => System.Diagnostics.Debug.Print(a);
        public static Action<string, object[]> LogFormatFunc = (a, b) => System.Diagnostics.Debug.Print(a, b);
        //public static Action<bool> AssertFunc = x => UnityEngine.Debug.Assert(x);
        //public static Action<string, object[]> LogFunc = (a,b) => UnityEngine.Debug.LogFormat(a, b);

        public static void Assert(bool condition) => AssertFunc(condition);
        public static void Log() { }
        public static void Log(string format) => LogFunc(format);
        public static void LogFormat(string format, params object[] args) => LogFormatFunc(format, args);
    }
}