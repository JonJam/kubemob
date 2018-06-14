using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class NamespaceMappingProfile : Profile
    {
        public NamespaceMappingProfile()
        {
            this.CreateMap<k8s.Models.V1Namespace, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty,
                    r.Status.Phase));
        }
    }
}
