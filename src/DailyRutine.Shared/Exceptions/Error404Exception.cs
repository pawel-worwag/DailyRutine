using System;
using System.Runtime.Serialization;

namespace DailyRutine.Shared.Exceptions
{
    public class Error404Exception : Exception
    {
        public Error404Exception()
        {
        }

        public Error404Exception(string? message) : base(message)
        {
        }

        public Error404Exception(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected Error404Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

