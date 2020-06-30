using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ApplicationMiddleware.Hosting
{
    public static class HostsConfigurator
    {
        public static IWebHostBuilder AddJsonConfigurationFiles(this IWebHostBuilder builder, List<string> settingsFilenames)
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            var directory = new FileInfo(location.AbsolutePath).Directory.FullName;

            builder.ConfigureAppConfiguration((context, builder) =>
            {
                var env = context.HostingEnvironment;

                foreach (var settings in settingsFilenames)
                {
                    var jsonConfigBase = Path.Combine(directory, $"{settings}.json");
                    var jsonConfigEnv = Path.Combine(directory, $"{settings}.{env.EnvironmentName}.json");

                    builder
                    .AddJsonFile(jsonConfigBase, optional: false)
                    .AddJsonFile(jsonConfigEnv, optional: false);
                }

                builder.AddEnvironmentVariables();
            });

            return builder;
        }

        public static IHostBuilder AddJsonConfigurationFiles(this IHostBuilder builder, List<string> settingsFilenames)
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            var directory = new FileInfo(location.AbsolutePath).Directory.FullName;

            builder.ConfigureAppConfiguration((context, builder) =>
            {
                var env = context.HostingEnvironment;

                foreach (var settings in settingsFilenames)
                {
                    var jsonConfigBase = Path.Combine(directory, $"{settings}.json");
                    var jsonConfigEnv = Path.Combine(directory, $"{settings}.{env.EnvironmentName}.json");

                    builder
                    .AddJsonFile(jsonConfigBase, optional: false)
                    .AddJsonFile(jsonConfigEnv, optional: false);
                }

                builder.AddEnvironmentVariables();
            });

            return builder;
        }

        public static IHostBuilder CreateConsoleHostBuilder(string[] args, string logPath = @"C:\logs", string appName = null)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(host =>
                {
                    host.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                });
            return builder;
        }

        public static IHostBuilder CreateDefaultBuilderWithWebHostDefaults<T>(string[] args, string logPath = @"C:\logs", string appName = null) where T : class
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<T>();
                    webBuilder.UseStaticWebAssets();
                });

            return builder;
        }
    }
}
