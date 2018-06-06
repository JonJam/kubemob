using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            this.CreateMap<k8s.Models.V1OwnerReference, OwnerReference>()
                .ConstructUsing((o) => new OwnerReference(
                    o.Name,
                    o.Kind));}
    }
}
