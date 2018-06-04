using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class ReplicaSetMappingProfile : Profile
    {
        public ReplicaSetMappingProfile()
        {
            this.CreateMap<k8s.Models.V1ReplicaSet, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty,
                    $"{r.Status.AvailableReplicas.GetValueOrDefault(0)}/{r.Status.Replicas}"));

            this.CreateMap<k8s.Models.V1ReplicaSet, ReplicaSetDetail>();
        }
    }
}
