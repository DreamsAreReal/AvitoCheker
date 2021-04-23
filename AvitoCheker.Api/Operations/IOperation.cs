using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using AvitoCheker.Api.Operations.Parameters;
using AvitoCheker.Api.Operations.Returns;

namespace AvitoCheker.Api.Operations
{
    public interface IOperation
    {
        IOperationReturn Execute(IOperationParameter parameters);
        void SetClient(HttpClient client);
    }
}
