using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AvitoChecker.Core.Exceptions;

namespace AvitoChecker.Core.Storages
{
    class AccountStorage : AbstractStorage
    {
        private List<(string, string)> _accounts;
        private const string ProxyFileName = "accounts.txt";
        public AccountStorage() : base()
        {
            _accounts = new List<(string, string)>();
            if (!File.Exists($"{FileDirectory}/{ProxyFileName}"))
                File.WriteAllText($"{FileDirectory}/{ProxyFileName}", String.Empty);
            ReadFile();
        }

        public (string, string) Get()
        {
            var random = new Random();
            var account = _accounts[0];
            _accounts.Remove(account);
            return account;
        }

        private void ReadFile()
        {
            var data = File.ReadAllLines($"{FileDirectory}/{ProxyFileName}");

            if (data.Length == 0)
                throw new NeedAccountException();

            foreach (var str in data)
            {
                _accounts.Add((str.Split(':')[0], str.Split(':')[1]));
            }
        }
    }
}
