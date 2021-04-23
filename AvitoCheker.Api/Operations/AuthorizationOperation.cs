using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AvitoCheсker.Api.Exceptions;
using AvitoCheсker.Api.Operations.Parameters;
using AvitoCheсker.Api.Operations.Returns;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AvitoCheсker.Api.Operations
{
    public class AuthorizationOperation : IOperation
    {
        /// <summary>
        /// Authorization on site.
        /// </summary>
        /// <param name="client">Any client</param>
        /// <param name="parameters">Required AuthorizationParameter</param>
        /// <returns>AuthorizationReturn</returns>
        public async Task<IOperationReturn> Execute(HttpClient client, IOperationParameter parameters = null)
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

            if (JsonConvert.DeserializeObject(response) is JObject json)
            {
                var requestCode = json["status"]?.ToString();


                if (requestCode == RequestCodes.WrongData)
                    throw new WrongDataException();
                else if (requestCode == RequestCodes.WrongPassword)
                    throw new WrongPasswordException();
                else if (requestCode == RequestCodes.NeedSms)
                    throw new TwoAuthorAuthenticationException();
                else if (requestCode == RequestCodes.Blocked) 
                    throw new BlockedException();
                else if (requestCode == RequestCodes.PasswordWasReset)
                    throw new PasswordWasResetException();


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
