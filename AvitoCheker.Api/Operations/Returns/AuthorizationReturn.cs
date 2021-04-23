namespace AvitoCheсker.Api.Operations.Returns
{
    public class AuthorizationReturn : IOperationReturn
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Session { get; set; }

    }
}
