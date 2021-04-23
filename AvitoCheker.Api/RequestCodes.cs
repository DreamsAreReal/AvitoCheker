namespace AvitoCheсker.Api
{
    static class RequestCodes
    {
        internal const string WrongData = "incorrect-data";
        internal const string WrongPassword = "wrong-credentials";
        internal const string NeedSms = "tfa-check";
        internal const string Blocked = "blocked-account";
        internal const string PasswordWasReset = "error-dialog";
        internal const string Unauthenticated = "unauthenticated";
        internal const string Success = "ok";
    }
}
