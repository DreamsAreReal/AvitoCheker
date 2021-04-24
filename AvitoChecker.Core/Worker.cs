using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AvitoCheсker.Api;
using AvitoCheсker.Api.Operations;
using AvitoCheсker.Api.Operations.Parameters;
using AvitoCheсker.Api.Operations.Returns;

namespace AvitoChecker.Core
{
    class Worker : IDisposable
    {

        private readonly Client _client;

        public Worker(Client client)
        {
            _client = client;
        }

        public async Task<(string, string, string, bool)>  DoWork(string login, string password)
        {
            AuthorizationOperation authorization = new AuthorizationOperation();
            AuthorizationParameter authorizationParameter = new AuthorizationParameter()
            {
                Username = login,
                Password = password
            };

            var authResponse = (AuthorizationReturn)(await _client.ExecuteOperation(authorization, authorizationParameter));

            GetPhoneInformationOperation getPhoneInformation = new GetPhoneInformationOperation();

            var phoneResponse = (ListPhonesReturn)(await _client.ExecuteOperation(getPhoneInformation));

            var isVerified = false;

            foreach (var phone in phoneResponse.Phones)
            {
                if (phone.IsValid)
                    isVerified = true;
            }

            return (authResponse.Session, login, password, isVerified);

        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
