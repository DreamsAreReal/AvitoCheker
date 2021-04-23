using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AvitoChecker.Core.Exceptions;

namespace AvitoChecker.Core.Storages
{
    class ProxyStorage : AbstractStorage
    {
        private List<(string,string)> _proxy;
        private const string ProxyFileName = "proxy.txt";
        public ProxyStorage() : base()
        {
            if(!File.Exists($"{FileDirectory}/{ProxyFileName}"))
                File.WriteAllText($"{FileDirectory}/{ProxyFileName}", String.Empty);
            ReadFile();
        }

        public (string,string) Get()
        {
            var random = new Random();
            return _proxy[random.Next(0, _proxy.Count - 1)];
        }

        private void ReadFile()
        {
            var data = File.ReadAllLines($"{FileDirectory}/{ProxyFileName}");

            if (data.Length == 0)
                throw new NeedProxyException();

            foreach (var str in data)
            { 
                _proxy.Add((str.Split(':')[0], str.Split(':')[1]));
            }
        }
    }
}
