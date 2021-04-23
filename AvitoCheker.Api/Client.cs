using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AvitoCheker.Api
{
    public class Client : IDisposable
    {

        private readonly HttpClient _client;
        private readonly HttpClientHandler _handler;

        public Client(IWebProxy proxy)
        {
           

            var cookie = new CookieContainer();
            _handler = new HttpClientHandler {CookieContainer = cookie};
            if (proxy != null)
                _handler.Proxy = proxy;

            _client = new HttpClient(_handler);
        }

        public void ExecuteOperation()
        {

        }


        public void Dispose()
        {
            _client?.Dispose();
            _handler?.Dispose();
        }
    }
}
