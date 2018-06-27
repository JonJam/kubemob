using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes.Extensions;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class ReplicationControllerMappingProfile : Profile
    {
        public ReplicationControllerMappingProfile()
        {
            this.CreateMap<k8s.Models.V1ReplicationController, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty,
                    $"{r.Status.AvailableReplicas.GetValueOrDefault(0)}/{r.Spec.Replicas}"));

            this.CreateMap<k8s.Models.V1ReplicationController, ReplicationControllerDetail>()
                .ConstructUsing((r) =>
            {
                List<string> labels = r.Metadata.Labels.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList();
                List<string> annotations = r.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                           new List<string>();

                List<string> selectors = r.Spec.Selector.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList();

                string creationTime = r.Metadata.CreationTimestamp.HasValue
                    ? $"{r.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                    : string.Empty;

                List<string> images = r.Spec.Template.Spec.Containers.Select(c => c.Image).ToList();

                string pods = string.Format(
                    AppResources.Detail_Pods,
                    r.Status.AvailableReplicas.GetValueOrDefault(0));

                string relatedSelector = r.Spec.Selector.ToRelatedSelector();

                return new ReplicationControllerDetail(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty,
                    labels.AsReadOnly(),
                    annotations.AsReadOnly(),
                    creationTime,
                    selectors.AsReadOnly(),
                    images.AsReadOnly(),
                    pods,
                    relatedSelector);
            });
        }
    }
}
