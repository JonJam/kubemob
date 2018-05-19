using System;
using System.Runtime.Serialization;

namespace KubeMob.Common.Exceptions
{
    [Serializable]
    public class ObjectTypeNotSupportedException : Exception
    {
        public ObjectTypeNotSupportedException()
        {
        }

        public ObjectTypeNotSupportedException(string message)
            : base(message)
        {
        }

        public ObjectTypeNotSupportedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ObjectTypeNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
