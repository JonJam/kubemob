using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes.Extensions;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class ReplicaSetMappingProfile : Profile
    {
        public ReplicaSetMappingProfile()
        {
            this.CreateMap<k8s.Models.V1ReplicaSet, ObjectSummary>()
                .ConstructUsing((r, rc) =>
                {
                    Status status = rc.GetStatus(r.Metadata.Uid);

                    return new ObjectSummary(
                        r.Metadata.Name,
                        r.Metadata.NamespaceProperty,
                        status);
                });

            this.CreateMap<k8s.Models.V1ReplicaSet, ReplicaSetDetail>()
                .ConstructUsing((r) =>
            {
                List<MetadataItem> labels = r.Metadata.Labels?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                            new List<MetadataItem>();
                List<MetadataItem> annotations = r.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                 new List<MetadataItem>();

                List<string> selectors = r.Spec.Selector.MatchLabels.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList();

                string creationTime = r.Metadata.CreationTimestamp.HasValue
                    ? $"{r.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                    : string.Empty;

                List<string> images = r.Spec.Template.Spec.Containers.Select(c => c.Image).ToList();

                string pods = string.Format(
                    AppResources.Detail_Pods,
                    r.Status.AvailableReplicas.GetValueOrDefault(0));

                string relatedSelector = r.Spec.Selector.ToRelatedSelector();

                return new ReplicaSetDetail(
                    r.Metadata.Uid,
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty,
                    labels.AsReadOnly(),
                    annotations.AsReadOnly(),
                    creationTime,
                    selectors.AsReadOnly(),
                    string.Join(", ", images),
                    pods,
                    relatedSelector);
            });
        }
    }
}
