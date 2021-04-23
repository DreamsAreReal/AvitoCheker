using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Fluent;

namespace AvitoChecker.Core
{
    public class Test
    {
        public void TestLog()
        {
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");
            LogManager.GetCurrentClassLogger().Log(LogLevel.Error, "Error");
            LogManager.GetCurrentClassLogger().Log(LogLevel.Debug, "Debug");
            LogManager.GetCurrentClassLogger().Log(LogLevel.Fatal, "Fatal");
            LogManager.GetCurrentClassLogger().Log(LogLevel.Info, "Info");
            LogManager.GetCurrentClassLogger().Log(LogLevel.Warn, "Warn");
        }
    }
}
