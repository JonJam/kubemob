using System;
using System.Runtime.Serialization;

namespace KubeMob.Common.Exceptions
{
    [Serializable]
    public class ClusterNotFoundException : Exception
    {
        public ClusterNotFoundException()
        {
        }

        public ClusterNotFoundException(string message)
            : base(message)
        {
        }

        public ClusterNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ClusterNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
