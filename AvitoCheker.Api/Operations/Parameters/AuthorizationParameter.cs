using System;
using System.Collections.Generic;
using System.Text;

namespace AvitoCheker.Api.Operations.Parameters
{
    public class AuthorizationParameter : IOperationParameter
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
