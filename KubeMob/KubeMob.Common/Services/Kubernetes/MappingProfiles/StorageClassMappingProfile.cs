using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class StorageClassMappingProfile : Profile
    {
        public StorageClassMappingProfile()
        {
            this.CreateMap<k8s.Models.V1StorageClass, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1StorageClass, StorageClassDetail>()
                .ConstructUsing((n) =>
                {
                    List<string> labels = n.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<string> annotations = n.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                               new List<string>();

                    string creationTime = n.Metadata.CreationTimestamp.HasValue
                        ? $"{n.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    // TODO expand
                    return new StorageClassDetail(
                        n.Metadata.Name,
                        n.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime);
                });

        }
    }
}
