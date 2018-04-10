using AutoMapper;
using KubeMob.Common.Services.Kubernetes;
using Microsoft.Azure.Management.ContainerService.Fluent;

namespace KubeMob.Common
{
    public static class MapperBootstrapper
    {
        public static void Configure()
        {
            // IConfigured linker to skip the following otherwise causes this to fail:
            // - AutoMapper
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<IKubernetesCluster, ClusterSummary>()
                    .ConstructUsing((kc) => new ClusterSummary(kc.Id, kc.Name));
            });
        }
    }
}
