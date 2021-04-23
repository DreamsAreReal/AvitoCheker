using System;
using System.Collections.Generic;
using System.Text;

namespace AvitoCheker.Api.Operations.Returns
{
    public class ListPhonesReturn : IOperationReturn
    {
        private List<PhoneReturn> Phones { get; set; }
    }
}
