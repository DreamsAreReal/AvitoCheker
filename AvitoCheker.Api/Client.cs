using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AvitoCheker.Api.Operations;
using AvitoCheker.Api.Operations.Parameters;
using AvitoCheker.Api.Operations.Returns;

namespace AvitoCheker.Api
{
    public class Client : IDisposable
    {

        private readonly HttpClient _client;
        private readonly HttpClientHandler _handler;

        public Client(IWebProxy proxy = null)
        {
           

            var cookie = new CookieContainer();
            _handler = new HttpClientHandler {CookieContainer = cookie};
            if (proxy != null)
                _handler.Proxy = proxy;

            _client = new HttpClient(_handler);
        }

        public async Task<IOperationReturn> ExecuteOperation(IOperation operation, IOperationParameter parameters)
        {
            return await operation.Execute(parameters, _client);
        }


        public void Dispose()
        {
            _client?.Dispose();
            _handler?.Dispose();
        }
    }
}
