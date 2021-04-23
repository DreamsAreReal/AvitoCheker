using System;
using System.Net.Http;
using System.Threading.Tasks;
using AvitoCheсker.Api.Operations.Parameters;
using AvitoCheсker.Api.Operations.Returns;

namespace AvitoCheсker.Api.Operations
{
    public interface IOperation : IDisposable
    {
        Task<IOperationReturn> Execute(HttpClient client, IOperationParameter parameters = null);

    }
}
