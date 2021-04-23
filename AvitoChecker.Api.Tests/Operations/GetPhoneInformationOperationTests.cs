using System;
using AvitoCheker.Api;
using AvitoCheker.Api.Exceptions;
using AvitoCheker.Api.Operations;
using AvitoCheker.Api.Operations.Parameters;
using AvitoCheker.Api.Operations.Returns;
using AvitoCheker.Api.Properties;
using NUnit.Framework;

namespace AvitoChecker.Api.Tests.Operations
{
    class GetPhoneInformationOperationTests
    {
        private Client _client;

        [SetUp]
        public void Setup()
        {
            _client = new Client();
        }


        [TestCase(3)]
        public void ExecuteTests(int accountMock)
        {
            var data = AccountMock.Get(accountMock);

            AuthorizationParameter authorizationParameter = new AuthorizationParameter
            {
                Username = data.Item1,
                Password = data.Item2
            };
            AuthorizationOperation authorizationOperation = new AuthorizationOperation();
            _client.ExecuteOperation(authorizationOperation, authorizationParameter).Wait();

            GetPhoneInformationOperation getPhoneInformationOperation = new GetPhoneInformationOperation();
            
            


            var result = (ListPhonesReturn)_client.ExecuteOperation(getPhoneInformationOperation).Result;

            foreach (var phone in result.Phones)
            {
                if (string.IsNullOrEmpty(phone.Number))
                    Assert.Fail();
            }

            Assert.Pass();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public void ExecuteNegativeTests(int accountMock)
        {

            GetPhoneInformationOperation getPhoneInformationOperation = new GetPhoneInformationOperation();

            try
            {
                _client.ExecuteOperation(getPhoneInformationOperation, null).Wait();
            }

            catch (AggregateException ex)
            {
                foreach (var exception in ex.InnerExceptions)
                {
                    if (exception is NeedAuthorizationException)
                    {
                        Assert.Pass();
                    }
                }
                Assert.Fail($"Was {ex}");
            }


        }
    }
}
