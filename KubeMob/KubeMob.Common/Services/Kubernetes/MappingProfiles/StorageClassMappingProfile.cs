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
                .ConstructUsing((s) => new ObjectSummary(
                    s.Metadata.Name,
                    s.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1StorageClass, StorageClassDetail>()
                .ConstructUsing((s) =>
                {
                    List<string> labels = s.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<MetadataItem> annotations = s.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                     new List<MetadataItem>();

                    string creationTime = s.Metadata.CreationTimestamp.HasValue
                        ? $"{s.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<string> parameters = s.Parameters.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList();

                    return new StorageClassDetail(
                        s.Metadata.Uid,
                        s.Metadata.Name,
                        s.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        s.Provisioner,
                        parameters);
                });
        }
    }
}
