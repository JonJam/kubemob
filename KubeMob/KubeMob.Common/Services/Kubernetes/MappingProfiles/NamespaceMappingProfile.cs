using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class NamespaceMappingProfile : Profile
    {
        public NamespaceMappingProfile()
        {
            this.CreateMap<k8s.Models.V1Namespace, ObjectSummary>()
                .ConstructUsing((n) =>
                {
                    Status status = Status.None;

                    switch (n.Status.Phase)
                    {
                        case "Active":
                            status = Status.Success;
                            break;
                        case "Terminating":
                            status = Status.Error;
                            break;
                        default:
                            break;
                    }

                    return new ObjectSummary(
                        n.Metadata.Name,
                        n.Metadata.NamespaceProperty,
                        status);
                });

            this.CreateMap<k8s.Models.V1Namespace, NamespaceDetail>()
                .ConstructUsing((n) =>
                {
                    List<string> labels = n.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<MetadataItem> annotations = n.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                               new List<MetadataItem>();

                    string creationTime = n.Metadata.CreationTimestamp.HasValue
                        ? $"{n.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    return new NamespaceDetail(
                        n.Metadata.Uid,
                        n.Metadata.Name,
                        n.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        n.Status.Phase);
                });
        }
    }
}
