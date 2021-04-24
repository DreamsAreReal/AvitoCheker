using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AvitoChecker.Core.Storages;
using AvitoChecker.UI;
using AvitoCheсker.Api;
using AvitoCheсker.Api.Exceptions;
using Microsoft.Extensions.Logging;
using NLog.Fluent;

namespace AvitoChecker.Core
{
    class RunTasks : IDisposable
    {
        private readonly ILogger<RunTasks> _logger;
        private readonly ProxyStorage _proxyStorage;
        private readonly AccountStorage _accountStorage;
        private readonly int _maxThreadCount = Startup.MaxThreadCount;
        private readonly ConcurrentDictionary<Guid, Thread> _threads;


        public RunTasks(ILogger<RunTasks> logger)
        {
            _proxyStorage = new ProxyStorage();
            _accountStorage = new AccountStorage();
            _logger = logger;
            _threads = new ConcurrentDictionary<Guid, Thread>();
        }

        public void Start()
        {
            while (0 < _accountStorage.Count)
            {
                while (_maxThreadCount > _threads.Count && _threads.Count<=_accountStorage.Count)
                { ;
                    var account = _accountStorage.Get();
                    var guid = Guid.NewGuid();
                    Thread thread = new Thread((() => LaunchTask(account.Item1, account.Item2, guid)));
                    while (!_threads.TryAdd(guid, thread))
                    {
                        Thread.Sleep(10);
                    }
                    thread.Start();
                }

            }

            while (_threads.Count!=0)
            {
                
                Thread.Sleep(10);
                     
            }

            Dispose();

        }

        

        private void LaunchTask(string login, string password, Guid guid)
        {

            var proxyFull = _proxyStorage.Get();
            var proxy = proxyFull.Item2.Item1;
            var port = proxyFull.Item2.Item2;
            
            IWebProxy pr = new WebProxy($"http://{proxy}:{port}", false);
            Client client = new Client(pr);
            Worker worker = new Worker(client);

            try
            {
                var result = worker.DoWork(login, password).Result;
                if (result.Item4)
                {
                    _logger.LogWarning($"{result.Item1}:{result.Item2}:{result.Item3}");
                }
                else
                {
                    _logger.LogInformation($"{result.Item1}:{result.Item2}:{result.Item3}");
                }
            }
            catch (AggregateException ex)
            {

                foreach (var exception in ex.InnerExceptions)
                {
                    if (exception is WrongPasswordException)
                        _logger.LogDebug($"{login} неверный пароль");
                    else if (exception is BlockedException)
                        _logger.LogDebug($"{login} аккаунт заблокирован");
                    else if (exception is NeedAuthorizationException)
                        _logger.LogDebug($"{login} требуется авторизация");
                    else if (exception is TwoAuthorAuthenticationException)
                        _logger.LogDebug($"{login} требуется код подтверждение с телефона");
                    else if (exception is WrongDataException)
                        _logger.LogDebug($"{login} неверные данные");
                    else if (exception is PasswordWasResetException)
                        _logger.LogDebug($"{login} сработала защита, пароль выслан на email");
                    else if (exception is HttpRequestException)
                    {
                        _logger.LogError($"Не рабочее прокси {proxy}:{port}");
                        _proxyStorage.Delete(proxyFull.Item1);
                        LaunchTask(login, password, guid);
                    }
                    else if (exception is IOException)
                    {
                        _logger.LogError($"Не рабочее прокси {proxy}:{port}");
                        _proxyStorage.Delete(proxyFull.Item1);
                        LaunchTask(login, password, guid);
                    }
                    else if (exception is SocketException)
                    {
                        _logger.LogError($"Не рабочее прокси {proxy}:{port}");
                        _proxyStorage.Delete(proxyFull.Item1);
                        LaunchTask(login, password, guid);
                    }
                    else
                        _logger.LogError($"{exception}");

                }

            }
            finally
            {
                worker.Dispose();
                Thread.Sleep(TimeSpan.FromMinutes(2));
                while (!_threads.TryRemove(guid, out var _))
                {
                    Thread.Sleep(10);
                }
                
            }


        }


        public void Dispose()
        {
            _logger.LogDebug("Завершено");
            foreach (var item in _threads)
            {
                item.Value.Interrupt();
            }
        }
    }
}
