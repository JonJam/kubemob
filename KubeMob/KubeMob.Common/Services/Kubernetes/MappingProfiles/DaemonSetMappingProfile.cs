using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using k8s.Models;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes.Extensions;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class DaemonSetMappingProfile : Profile
    {
        public DaemonSetMappingProfile()
        {
            this.CreateMap<V1DaemonSet, ObjectSummary>()
                .ConstructUsing((d, rc) =>
                {
                    Status status = rc.GetStatus(d.Metadata.Uid);

                    return new ObjectSummary(
                        d.Metadata.Name,
                        d.Metadata.NamespaceProperty,
                        status);
                });

            this.CreateMap<V1DaemonSet, DaemonSetDetail>()
                .ConstructUsing((d) =>
                {
                    List<MetadataItem> labels = d.Metadata.Labels?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                new List<MetadataItem>();
                    List<MetadataItem> annotations = d.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                     new List<MetadataItem>();

                    string creationTime = d.Metadata.CreationTimestamp.HasValue
                        ? $"{d.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<string> images = d.Spec.Template.Spec.Containers.Select(c => c.Image).ToList();

                    string pods = string.Format(
                        AppResources.Detail_Pods,
                        d.Status.CurrentNumberScheduled);

                    string relatedSelector = d.Spec.Selector.ToRelatedSelector();

                    return new DaemonSetDetail(
                        d.Metadata.Uid,
                        d.Metadata.Name,
                        d.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        string.Join(", ", images),
                        pods,
                        relatedSelector);
                });
        }
    }
}
