using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AvitoCheсker.Api.Operations;
using AvitoCheсker.Api.Operations.Parameters;
using AvitoCheсker.Api.Operations.Returns;

namespace AvitoCheсker.Api
{
    public class Client : IDisposable
    {

        private readonly HttpClient _client;
        private readonly HttpClientHandler _handler;

        public Client(IWebProxy proxy)
        {
            var cookie = new CookieContainer();
            _handler = new HttpClientHandler();
            _handler.Proxy = proxy;
            _handler.UseCookies = true;
            _handler.UseProxy = true;

            _client = new HttpClient(_handler);
            _client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
            
        }

        public async Task<IOperationReturn> ExecuteOperation(IOperation operation, IOperationParameter parameters = null)
        {
            var result = await operation.Execute(_client, parameters);
            operation.Dispose();
            return result;
        }


        public void Dispose()
        {
            _client?.Dispose();
            _handler?.Dispose();
        }
    }
}
