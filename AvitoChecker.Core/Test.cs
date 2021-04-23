using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Fluent;

namespace AvitoChecker.Core
{
    public class Test
    {
        private readonly ILogger<Test> _logger;

        public Test(ILogger<Test> logger)
        {
            _logger = logger;
        }

        public void DoAction(string name)
        {
            _logger.LogDebug(20, "Doing hard work! {Action}", name);
        }
    }
}
