using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class ConfigMapMappingProfile : Profile
    {
        public ConfigMapMappingProfile()
        {
            this.CreateMap<k8s.Models.V1ConfigMap, ObjectSummary>()
                .ConstructUsing((c) => new ObjectSummary(
                    c.Metadata.Name,
                    c.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1ConfigMap, ConfigMapDetail>()
                .ConstructUsing((c) =>
                {
                    List<string> labels = c.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<MetadataItem> annotations = c.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                     new List<MetadataItem>();

                    string creationTime = c.Metadata.CreationTimestamp.HasValue
                        ? $"{c.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<KeyValuePair<string, string>> data = c.Data?.ToList() ?? new List<KeyValuePair<string, string>>();

                    return new ConfigMapDetail(
                        c.Metadata.Uid,
                        c.Metadata.Name,
                        c.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        data.AsReadOnly());
                });
        }
    }
}
