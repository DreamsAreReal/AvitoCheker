using System;
using System.Collections.Generic;
using System.Net.Http;
using AvitoCheker.Api.Exceptions;
using AvitoCheker.Api.Operations.Parameters;
using AvitoCheker.Api.Operations.Returns;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading.Tasks;

namespace AvitoCheker.Api.Operations
{
    public class AuthorizationOperation : IOperation
    {
        public async Task<IOperationReturn> Execute(IOperationParameter parameters, HttpClient client)
        {
            var data = (AuthorizationParameter) parameters;

            if (String.IsNullOrEmpty(data.Username)
                && String.IsNullOrEmpty(data.Password))
            {
                throw new WrongDataException();
            }
            

            Dictionary<string, string> requestParams = new Dictionary<string, string>
            {
                { "key",  Settings.AppKey},
                { "login", data.Username},
                { "password", data.Password }
            };

            var response = await (await client.PostAsync(Routes.BaseUrl + Routes.AuthUrl, new FormUrlEncodedContent(requestParams))).Content.ReadAsStringAsync();
        }
    }
}
