using System;
using System.Collections.Generic;
using System.Text;
using AvitoCheker.Api;
using AvitoCheker.Api.Operations;
using AvitoCheker.Api.Operations.Parameters;
using NUnit.Framework;

namespace AvitoChecker.Api.Tests.Operations
{
    class AuthorizationOperationTests
    {
        private Client _client; 

        [OneTimeSetUp]
        public void Setup()
        {
            _client = new Client();
        }

        [TestCase(1)]
        public void ExecuteTests((string, string) data)
        {
            AuthorizationOperation authorizationOperation = new AuthorizationOperation();
            AuthorizationParameter authorizationParameter = new AuthorizationParameter
            {
                Username = data.Item1,
                Password = data.Item2
            };
            
            
            var result = _client.ExecuteOperation(authorizationOperation, authorizationParameter);
        }

    }
}
