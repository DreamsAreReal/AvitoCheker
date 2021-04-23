using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AvitoCheker.Api.Operations.Parameters;
using AvitoCheker.Api.Operations.Returns;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AvitoCheker.Api.Operations
{
    class GetPhoneInformationOperation : IOperation
    {
        /// <summary>
        /// Get phone information from phone settings page.
        /// Call after authorization
        /// </summary>
        /// <param name="client">HttpClient with session cookie.</param>
        /// <param name="parameters">Not required</param>
        /// <returns></returns>
        public async Task<IOperationReturn> Execute(HttpClient client, IOperationParameter parameters = null)
        {
            var response = await (await client.GetAsync(Routes.BaseUrl + Routes.PhoneSettingsUrl)).Content.ReadAsStringAsync();
            
            if(JsonConvert.DeserializeObject(response) is JObject json)
            {
                var status = json["status"]?.ToString();
                var phones = json["result"]?["phones"];

                if (status == RequestCodes.Success && phones != null)
                {
                    var answer = new ListPhonesReturn()
                    {
                        Phones = new List<PhoneReturn>(),
                    };

                    foreach (var phone in phones)
                    {
                        bool.TryParse(json["result"]?["phones"]?["verified"]?.ToString(), out var isValid);
                        answer.Phones.Add(new PhoneReturn()
                        {
                            Number = json["result"]?["phones"]?["phone"]?.ToString(),
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
