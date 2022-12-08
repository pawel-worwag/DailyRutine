using System;
using System.Runtime.Serialization;

namespace DailyRutine.Shared.Exceptions
{
    public class Error400Exception : Exception
    {
        public Error400Exception() : base()
        {
        }

        public Error400Exception(string? message) : base(message)
        {
        }

        public Error400Exception(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected Error400Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

