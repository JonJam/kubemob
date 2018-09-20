using AutoMapper;
using k8s.Models;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            this.CreateMap<V1Role, ObjectSummary>()
                .ConstructUsing((c) => new ObjectSummary(
                        c.Metadata.Name,
                        c.Metadata.NamespaceProperty));
        }
    }
}
