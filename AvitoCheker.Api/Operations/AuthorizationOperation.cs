using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using AvitoCheker.Api.Exceptions;
using AvitoCheker.Api.Operations.Parameters;
using AvitoCheker.Api.Operations.Returns;

namespace AvitoCheker.Api.Operations
{
    public class AuthorizationOperation : IOperation
    {
        public IOperationReturn Execute(IOperationParameter parameters, HttpClient client)
        {
            var data = (AuthorizationParameter) parameters;

            if (String.IsNullOrEmpty(data.Username)
                && String.IsNullOrEmpty(data.Password))
            {
                throw new WrongDataException();
            }

            
        }
    }
}
