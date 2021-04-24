using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using AvitoChecker.Core.Exceptions;
using NLog;

namespace AvitoChecker.Core.Storages
{
    class ProxyStorage : AbstractStorage
    {
        public int Count => _proxy.Count;
        private readonly ConcurrentDictionary<int, (string, string)> _proxy;
        private const string ProxyFileName = "proxy.txt";
        public ProxyStorage() : base()
        {
            _proxy = new ConcurrentDictionary<int, (string, string)>();
            if(!File.Exists($"{FileDirectory}/{ProxyFileName}"))
                File.WriteAllText($"{FileDirectory}/{ProxyFileName}", String.Empty);
            ReadFile();
        }

        public (int,(string,string)) Get()
        {
            var random = new Random();
            var id = random.Next(0, _proxy.Count - 1);
            return (id, _proxy[id]);
        }

        public void Delete(int id)
        {
            _proxy.Remove(id, out var _);
        }

        private void ReadFile()
        {
            var data = File.ReadAllLines($"{FileDirectory}/{ProxyFileName}");

            if (data.Length == 0)
            {
                LogManager.GetCurrentClassLogger().Error("Нужны прокси");
                throw new NeedProxyException();
            }

            for (int i = 0; i < data.Length; i++)
            {
                _proxy.TryAdd(i, (data[i].Split(':')[0], data[i].Split(':')[1]));
            }
        }
    }
}
