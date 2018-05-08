using System;
using Microsoft.Rest.TransientFaultHandling;

namespace KubeMob.Droid.Services.Kubernetes
{
    public class KubernetesTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
    {
        public bool IsTransient(Exception ex)
        {
            if (ex != null &&
                ex is Java.Net.UnknownHostException)
            {
                return true;
            }

            return false;
        }
    }
}