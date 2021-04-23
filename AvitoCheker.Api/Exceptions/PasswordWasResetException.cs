using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AvitoCheker.Api.Exceptions
{
    [Serializable]
    public class PasswordWasResetException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public PasswordWasResetException()
        {
        }

        public PasswordWasResetException(string message) : base(message)
        {
        }

        public PasswordWasResetException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PasswordWasResetException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
