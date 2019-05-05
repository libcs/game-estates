using System;

namespace Gamer.Core
{
    public class Debug
    {
        public static Action<bool> AssertFunc = x => System.Diagnostics.Debug.Assert(x);
        public static Action<object> LogFunc = x => System.Diagnostics.Debug.Print(x.ToString());
        //static Action<bool> AssertFunc = x => UnityEngine.Debug.Assert(x);
        //static Action<object> LogFunc = x => UnityEngine.Debug.Log(x);

        public static void Assert(bool condition) => AssertFunc(condition);
        public static void Log(object message) => LogFunc(message);
    }
}