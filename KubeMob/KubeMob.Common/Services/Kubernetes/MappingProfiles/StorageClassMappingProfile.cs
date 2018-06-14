using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class StorageClassMappingProfile : Profile
    {
        public StorageClassMappingProfile()
        {
            this.CreateMap<k8s.Models.V1StorageClass, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty));
        }
    }
}
