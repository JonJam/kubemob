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

                    // Note for a namespace n.Metadata.NamespaceProperty is null.
                    return new ObjectSummary(
                        n.Metadata.Name,
                        n.Metadata.Name,
                        status);
                });

            this.CreateMap<k8s.Models.V1Namespace, NamespaceDetail>()
                .ConstructUsing((n) =>
                {
                    List<MetadataItem> labels = n.Metadata.Labels?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                new List<MetadataItem>();
                    List<MetadataItem> annotations = n.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                               new List<MetadataItem>();

                    string creationTime = n.Metadata.CreationTimestamp.HasValue
                        ? $"{n.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;
                    
                    // Note for a namespace n.Metadata.NamespaceProperty is null.
                    return new NamespaceDetail(
                        n.Metadata.Uid,
                        n.Metadata.Name,
                        n.Metadata.Name,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        n.Status.Phase);
                });
        }
    }
}
