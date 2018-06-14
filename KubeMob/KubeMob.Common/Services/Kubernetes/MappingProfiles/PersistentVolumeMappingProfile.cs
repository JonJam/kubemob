using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class PersistentVolumeMappingProfile : Profile
    {
        public PersistentVolumeMappingProfile()
        {
            this.CreateMap<k8s.Models.V1PersistentVolume, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty,
                    r.Status.Phase));
        }
    }
}
