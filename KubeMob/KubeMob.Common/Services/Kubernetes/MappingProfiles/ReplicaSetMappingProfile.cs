using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Resx;
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

            this.CreateMap<k8s.Models.V1ReplicaSet, ReplicaSetDetail>()
                .ConstructUsing((r) =>
            {
                List<string> labels = r.Metadata.Labels.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList();
                List<string> annotations = r.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                           new List<string>();

                List<string> selectors = r.Spec.Selector.MatchLabels.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList();

                string creationTime = r.Metadata.CreationTimestamp.HasValue
                    ? $"{r.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                    : string.Empty;

                List<string> images = r.Spec.Template.Spec.Containers.Select(c => c.Image).ToList();

                string pods = string.Format(
                    AppResources.Detail_Pods,
                    r.Status.AvailableReplicas.GetValueOrDefault(0));

                string selector = string.Join(",", r.Spec.Selector.MatchLabels.Select(kvp => $"{kvp.Key}={kvp.Value}"));

                return new ReplicaSetDetail(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty,
                    labels.AsReadOnly(),
                    annotations.AsReadOnly(),
                    creationTime,
                    selectors.AsReadOnly(),
                    images.AsReadOnly(),
                    pods,
                    selector);
            });
        }
    }
}
