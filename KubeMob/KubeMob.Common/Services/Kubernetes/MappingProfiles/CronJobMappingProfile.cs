using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class CronJobMappingProfile : Profile
    {
        public CronJobMappingProfile()
        {
            this.CreateMap<k8s.Models.V1beta1CronJob, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty,
                    r.Spec.Schedule));

            this.CreateMap<k8s.Models.V1beta1CronJob, CronJobDetail>()
                .ConstructUsing((s) =>
                {
                    
                    return new CronJobDetail();
                });
        }
    }
}
