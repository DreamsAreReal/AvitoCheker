using System;
using System.Collections.Generic;
using System.Text;

namespace AvitoCheker.Api.Operations.Returns
{
    public class PhoneReturn : IOperationReturn
    {
        public string Number { get; set; }
        public bool IsValid { get; set; }

    }
}
