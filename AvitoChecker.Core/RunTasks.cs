using System;
using System.Collections.Generic;
using System.Text;
using AvitoChecker.Core.Storages;
using Microsoft.Extensions.Logging;

namespace AvitoChecker.Core
{
    class RunTasks
    {
        private readonly ILogger<RunTasks> _logger;
        private readonly ProxyStorage _proxyStorage;
        private readonly AccountStorage _accountStorage;
        private int maxThreadCount;

        public RunTasks(ILogger<RunTasks> logger, ProxyStorage proxyStorage, AccountStorage accountStorage)
        {
            _proxyStorage = proxyStorage;
            _accountStorage = accountStorage;
        }

        public void Start()
        {

        }
    }
}
