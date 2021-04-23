using System;
using System.Collections.Generic;
using System.Text;

namespace AvitoCheker.Api
{
    static class RequestCodes
    {
        internal const string WrongData = "incorrect-data";
        internal const string WrongPassword = "wrong-credentials";
        internal const string NeedSms = "tfa-check";
        internal const string Blocked = "blocked-account";
        internal const string Success = "ok";
    }
}
