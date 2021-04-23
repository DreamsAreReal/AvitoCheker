using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AvitoChecker.Core.Exceptions
{
    [Serializable]
    public class NeedAccountException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public NeedAccountException()
        {
        }

        public NeedAccountException(string message) : base(message)
        {
        }

        public NeedAccountException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NeedAccountException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
