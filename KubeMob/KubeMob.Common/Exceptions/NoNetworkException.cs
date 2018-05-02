using System;

namespace KubeMob.Common.Exceptions
{
    public class NoNetworkException : Exception
    {
        public NoNetworkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
