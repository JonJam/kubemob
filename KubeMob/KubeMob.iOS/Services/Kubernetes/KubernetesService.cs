using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using KubeMob.Common;
using KubeMob.Common.Exceptions;
using KubeMob.Common.Services.AccountManagement;
using KubeMob.Common.Services.Kubernetes;
using KubeMob.Common.Services.Settings;
using Xamarin.Forms.Internals;

[assembly: Xamarin.Forms.Dependency(typeof(KubeMob.iOS.Services.Kubernetes.KubernetesService))]
namespace KubeMob.iOS.Services.Kubernetes
{
    /// <summary>
    /// Created platform specific implementation as <see cref="k8s.Kubernetes"/> needs to perform platform specific exception handling.
    /// </summary>
    public class KubernetesService : KubernetesServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KubernetesService"/> class.
        /// 
        /// Since this is being constructed using <see cref="Xamarin.Forms.DependencyService"/> it has to be parameterless.
        /// Therefore using the ViewModelLocator to look up the dependencies.
        /// </summary>
        [Preserve]
        public KubernetesService()
            : base(ViewModelLocator.Resolve<IAppSettings>(), ViewModelLocator.Resolve<IEnumerable<IAccountManager>>())
        {
        }

        [Preserve]
        public KubernetesService(
            IAppSettings appSettings,
            IEnumerable<IAccountManager> accountManagers)
            : base(appSettings, accountManagers)
        {
        }

        protected override async Task<V1PodList> GetPods()
        {
            try
            {
                IKubernetes client = await this.Client.Value;

                return await client.ListPodForAllNamespacesAsync();
            }
            catch (Exception e)
            {
                // No internet.
                throw new NoNetworkException(e.Message, e);
            }
        }
    }
}
