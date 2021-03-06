using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class HorizontalPodAutoscalerMappingProfile : Profile
    {
        public HorizontalPodAutoscalerMappingProfile()
        {
            this.CreateMap<k8s.Models.V1HorizontalPodAutoscaler, ObjectSummary>()
                .ConstructUsing((h) => new ObjectSummary(
                    h.Metadata.Name,
                    h.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1HorizontalPodAutoscaler, HorizontalPodAutoscalerDetail>()
                .ConstructUsing((h) =>
                {
                    List<MetadataItem> labels = h.Metadata.Labels?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                new List<MetadataItem>();
                    List<MetadataItem> annotations = h.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                     new List<MetadataItem>();

                    string creationTime = h.Metadata.CreationTimestamp.HasValue
                        ? $"{h.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    ObjectReference target = Mapper.Map<ObjectReference>(h.Spec.ScaleTargetRef);

                    int minReplicas = h.Spec.MinReplicas.GetValueOrDefault(0);

                    string targetCpuUtilization = h.Spec.TargetCPUUtilizationPercentage.HasValue
                        ? $"{ h.Spec.TargetCPUUtilizationPercentage.Value}%"
                        : string.Empty;
                    string currentCpuUtilization = h.Status.CurrentCPUUtilizationPercentage.HasValue
                        ? $"{h.Status.CurrentCPUUtilizationPercentage.Value}%"
                        : string.Empty;
                    string lastScaled = h.Status.LastScaleTime.HasValue
                        ? $"{h.Status.LastScaleTime.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    return new HorizontalPodAutoscalerDetail(
                        h.Metadata.Uid,
                        h.Metadata.Name,
                        h.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        target,
                        minReplicas,
                        h.Spec.MaxReplicas,
                        targetCpuUtilization,
                        h.Status.CurrentReplicas,
                        h.Status.DesiredReplicas,
                        currentCpuUtilization,
                        lastScaled);
                });
        }
    }
}
