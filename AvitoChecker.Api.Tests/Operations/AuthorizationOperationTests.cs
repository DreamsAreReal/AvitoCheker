using System;
using System.Collections.Generic;
using AvitoCheсker.Api;
using AvitoCheсker.Api.Exceptions;
using AvitoCheсker.Api.Operations;
using AvitoCheсker.Api.Operations.Parameters;
using AvitoCheсker.Api.Operations.Returns;
using NUnit.Framework;

namespace AvitoChecker.Api.Tests.Operations
{
    class AuthorizationOperationTests
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
            AuthorizationOperation authorizationOperation = new AuthorizationOperation();
            AuthorizationParameter authorizationParameter = new AuthorizationParameter
            {
                Username = data.Item1,
                Password = data.Item2
            };
            
           
            var result = (AuthorizationReturn)_client.ExecuteOperation(authorizationOperation, authorizationParameter).Result;

            if(string.IsNullOrEmpty(result.Id) 
            && string.IsNullOrEmpty(result.Session))
                Assert.Fail();

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
            var data = AccountMock.Get(accountMock);
            AuthorizationOperation authorizationOperation = new AuthorizationOperation();
            AuthorizationParameter authorizationParameter = new AuthorizationParameter
            {
                Username = data.Item1,
                Password = data.Item2
            };


            try
            {
                _client.ExecuteOperation(authorizationOperation, authorizationParameter).Wait();
            }
         
            catch(AggregateException ex)
            {
                foreach (var exception in ex.InnerExceptions)
                {
                    if (exception is WrongDataException
                    || exception is BlockedException
                    || exception is TwoAuthorAuthenticationException
                    || exception is WrongPasswordException
                    || exception is PasswordWasResetException)
                    {
                        Assert.Pass();
                    }
                }
                Assert.Fail($"Was {ex}");
            }

            
        }


    }
}
