using System.Collections.Generic;

namespace AvitoCheсker.Api.Operations.Returns
{
    public class ListPhonesReturn : IOperationReturn
    {
        public List<PhoneReturn> Phones { get; set; }
    }
}
