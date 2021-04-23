using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AvitoCheker.Api.Operations.Parameters;
using AvitoCheker.Api.Operations.Returns;

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
        public Task<IOperationReturn> Execute(HttpClient client, IOperationParameter parameters = null)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
