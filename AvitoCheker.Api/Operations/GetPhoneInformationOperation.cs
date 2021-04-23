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
    public class GetPhoneInformationOperation : IOperation
    {
        /// <summary>
        /// Get phone information from phone settings page.
        /// Call after authorization
        /// </summary>
        /// <param name="client">HttpClient with session cookie.</param>
        /// <param name="parameters">Not required</param>
        /// <returns>ListPhonesReturn</returns>
        public async Task<IOperationReturn> Execute(HttpClient client, IOperationParameter parameters = null)
        {
            // Need to send request. 
            // If don't send key then will answer 403 code.
            var getParams = $"?key={Settings.AppKey}";
            var query = await client.GetAsync(Routes.BaseUrl + Routes.PhoneSettingsUrl + getParams);


            var response = await query.Content.ReadAsStringAsync();
            
           
            if (JsonConvert.DeserializeObject(response) is JObject json)
            {
                var status = json["status"]?.ToString();
                if (status == RequestCodes.Unauthenticated)
                    throw new NeedAuthorizationException();


                var phones = json["result"]?["phones"];

                if (status == RequestCodes.Success && phones != null)
                {
                    var answer = new ListPhonesReturn()
                    {
                        Phones = new List<PhoneReturn>(),
                    };

                    foreach (var phone in phones)
                    {
                        bool.TryParse(phone?["verified"]?.ToString(), out var isValid);
                        answer.Phones.Add(new PhoneReturn()
                        {
                            Number = phone?["phone"]?.ToString(),
                            IsValid = isValid,
                        });    
                    }

                    return answer;
                }
            }

            throw new Exception(response);
        }

        public void Dispose()
        {
        }
    }
}
