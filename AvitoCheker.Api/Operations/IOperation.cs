using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AvitoCheker.Api.Operations.Parameters;
using AvitoCheker.Api.Operations.Returns;

namespace AvitoCheker.Api.Operations
{
    public interface IOperation
    {
        Task<IOperationReturn> Execute(HttpClient client, IOperationParameter parameters = null);
    }
}
