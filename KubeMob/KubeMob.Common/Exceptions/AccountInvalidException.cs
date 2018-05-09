using System;
using System.Runtime.Serialization;

namespace KubeMob.Common.Exceptions
{
    [Serializable]
    public class AccountInvalidException : Exception
    {
        public AccountInvalidException()
        {
        }

        public AccountInvalidException(string message)
            : base(message)
        {
        }

        public AccountInvalidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AccountInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
