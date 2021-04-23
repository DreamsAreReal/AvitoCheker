﻿using System;
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
            var json = JsonConvert.DeserializeObject(response) as JObject;
            var requestCode = json["status"].ToString();


            if (requestCode == RequestCodes.WrongData)
                throw new WrongDataException();

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


            throw new Exception(response);

        }
    }
}
