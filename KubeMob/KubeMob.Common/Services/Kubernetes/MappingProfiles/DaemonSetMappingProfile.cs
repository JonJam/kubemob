using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class DaemonSetMappingProfile : Profile
    {
        public DaemonSetMappingProfile()
        {
            this.CreateMap<k8s.Models.V1DaemonSet, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty,
                    $"{r.Status.CurrentNumberScheduled}/{r.Status.DesiredNumberScheduled}"));

            this.CreateMap<k8s.Models.V1DaemonSet, DaemonSetDetail>()
                .ConstructUsing((c) =>
                {
                    return new DaemonSetDetail();
                });
        }
    }
}
