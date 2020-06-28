using Serilog;
using System;

namespace Middleware.Application.Logging
{
    public class L0G
    {
        public static string Inf(string e)
        {
            Log.Logger.Information(e);
            return e;
        }

        public static string Dbg(string e)
        {
            Log.Logger.Debug(e);
            return e;
        }

        public static string Err(string e)
        {
            Log.Logger.Error(e);
            return e;
        }

        public static string Warn(string e)
        {
            Log.Logger.Warning(e);
            return e;
        }

        public static string Fatal(string e)
        {
            Log.Logger.Fatal(e);
            return e;
        }

        public static string Exception(Exception e)
        {
            return Fatal(e.Message);
        }

        public static string Exception(Exception e, string message)
        {
            var m = $"Exception {message}, {e.Message}";
            return Fatal(m);
        }
    }
}