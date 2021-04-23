using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AvitoCheker.Api.Exceptions
{
    [Serializable]
    public class WrongDataException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public WrongDataException()
        {
        }

        public WrongDataException(string message) : base(message)
        {
        }

        public WrongDataException(string message, Exception inner) : base(message, inner)
        {
        }

        protected WrongDataException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
    
}
