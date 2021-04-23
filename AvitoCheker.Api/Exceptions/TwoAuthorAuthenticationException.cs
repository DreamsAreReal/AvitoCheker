using System;
using System.Runtime.Serialization;

namespace AvitoCheсker.Api.Exceptions
{
    [Serializable]
    public class TwoAuthorAuthenticationException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public TwoAuthorAuthenticationException()
        {
        }

        public TwoAuthorAuthenticationException(string message) : base(message)
        {
        }

        public TwoAuthorAuthenticationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TwoAuthorAuthenticationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
