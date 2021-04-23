using System;
using System.Collections.Generic;
using System.Text;
using AvitoChecker.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace AvitoChecker.UI
{
    public static class Startup
    {
        public static void Configure()
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
                    .Build();

                var servicesProvider = GetServiceCollection(config);
                using (servicesProvider as IDisposable)
                {
                    var runner = servicesProvider.GetRequiredService<Test>();
                    runner.DoAction("Action1");

                    Console.WriteLine("Press ANY key to exit");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                logger(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }
        private static IServiceProvider GetServiceCollection(IConfiguration config)
        {
            return new ServiceCollection()
                .AddTransient<Test>()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddNLog(config);
                })
                .BuildServiceProvider();
        }
    }
}
