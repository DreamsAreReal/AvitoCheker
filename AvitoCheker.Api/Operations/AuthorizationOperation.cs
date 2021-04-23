using System;
using System.Collections.Generic;
using System.Net.Http;
using AvitoCheker.Api.Exceptions;
using AvitoCheker.Api.Operations.Parameters;
using AvitoCheker.Api.Operations.Returns;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AvitoCheker.Api.Operations
{
    public class AuthorizationOperation : IOperation
    {
        /// <summary>
        /// Authorization on site.
        /// </summary>
        /// <param name="client">Any client</param>
        /// <param name="parameters">Required AuthorizationParameter</param>
        /// <returns></returns>
        public async Task<IOperationReturn> Execute(HttpClient client, IOperationParameter parameters = null)
        {
            var data = (AuthorizationParameter) parameters;

            if (String.IsNullOrEmpty(data.Username)
                && String.IsNullOrEmpty(data.Password))
            {
                throw new AccountNoValidException();
            }
            

            Dictionary<string, string> requestParams = new Dictionary<string, string>
            {
                { "key",  Settings.AppKey},
                { "login", data.Username},
                { "password", data.Password }
            };

            var response = await (await client.PostAsync(Routes.BaseUrl + Routes.AuthUrl, new FormUrlEncodedContent(requestParams))).Content.ReadAsStringAsync();

            if (JsonConvert.DeserializeObject(response) is JObject json)
            {
                var requestCode = json["status"]?.ToString();


                if (requestCode == RequestCodes.WrongData 
                    || requestCode==RequestCodes.WrongPassword 
                    || requestCode==RequestCodes.NeedSms
                    || requestCode==RequestCodes.Blocked)
                    throw new AccountNoValidException();

                if (requestCode == RequestCodes.Success)
                    return new AuthorizationReturn
                    {
                        Id = json["result"]?["user"]?["id"]?.ToString(),
                        Name = json["result"]?["user"]?["name"]?.ToString(),
                        Type = json["result"]?["user"]?["type"]?["name"]?.ToString(),
                        Email = json["result"]?["type"]?["email"]?.ToString(),
                        Phone = json["result"]?["type"]?["phone"]?.ToString(),
                        Session = json["result"]?["session"]?.ToString(),
                    };
            }


            throw new Exception(response);

        }

        public void Dispose()
        {
        }
    }
}
