using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Reflection;

namespace Middleware.Application.Logging
{
    public static class SerilogConfigurator
    {
        public static void CreateLogger(string logPath, string appName) => CreateLogger(logPath, string.Empty, appName);

        public static void CreateLogger(string logPath, string folderName, string appName)
        {
            if (string.IsNullOrEmpty(appName))
            {
                appName = Assembly.GetEntryAssembly().GetName().Name;
            }

            var process = Process.GetCurrentProcess();
            bool isIIS = string.Compare(process.ProcessName, "w3wp") == 0;

            string logFoldername = string.IsNullOrEmpty(folderName) 
                ? (isIIS ? $"IIS-{appName}" : process.ProcessName) 
                : folderName;

            var logFilepath = $"{logPath}\\{logFoldername}\\{appName}_.log";

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .WriteTo.ColoredConsole(LogEventLevel.Information)
                .WriteTo.File(logFilepath, rollingInterval: RollingInterval.Day)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
                .CreateLogger();
        }

        public static IHostBuilder AddSerilog(this IHostBuilder hostBuilder, string logPath = @"C:\logs", string appName = null) => 
            AddSerilog(hostBuilder, logPath, string.Empty, appName);

        public static IHostBuilder AddSerilog(this IHostBuilder hostBuilder, string logPath, string logFoldername, string appName)
        {
            CreateLogger(logPath, logFoldername, appName);
            return hostBuilder.UseSerilog();
        }

        public static IWebHostBuilder AddSerilog(this IWebHostBuilder hostBuilder, string logPath = @"C:\logs", string appName = null)
        {
            CreateLogger(logPath, appName);
            return hostBuilder.UseSerilog();
        }
    }
}
