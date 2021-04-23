namespace AvitoCheсker.Api.Operations.Returns
{
    public class PhoneReturn : IOperationReturn
    {
        public string Number { get; set; }
        public bool IsValid { get; set; }

    }
}
