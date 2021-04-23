using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AvitoCheker.Api.Exceptions
{
    [Serializable]
    public class AccountNoValidException : ArgumentException
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public AccountNoValidException()
        {
        }

        public AccountNoValidException(string message) : base(message)
        {
        }

        public AccountNoValidException(string message, Exception inner) : base(message, inner)
        {
        }

        protected AccountNoValidException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
    
}
