using System;
using System.Collections.Generic;
using System.Text;
using AvitoChecker.Core;
using AvitoChecker.Core.Storages;
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
            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .Build();

            var servicesProvider = GetServiceCollection(config);
            using (servicesProvider as IDisposable)
            {
                var runner = servicesProvider.GetRequiredService<Test>();
                runner.DoAction("Action1");
            }
        }
        private static IServiceProvider GetServiceCollection(IConfiguration config)
        {
            return new ServiceCollection()
                .AddTransient<ProxyStorage>()
                .AddTransient<AccountStorage>()
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
