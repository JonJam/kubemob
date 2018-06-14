using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class NodeMappingProfile : Profile
    {
        public NodeMappingProfile()
        {
            this.CreateMap<k8s.Models.V1Node, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty));
        }
    }
}
