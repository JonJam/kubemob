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
            //TODO status
            this.CreateMap<k8s.Models.V1ConfigMap, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1ConfigMap, ConfigMapDetail>()
                .ConstructUsing((c) =>
                {
                    List<string> labels = c.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<string> annotations = c.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                               new List<string>();

                    string creationTime = c.Metadata.CreationTimestamp.HasValue
                        ? $"{c.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<KeyValuePair<string, string>> data = c.Data?.ToList() ?? new List<KeyValuePair<string, string>>();

                    return new ConfigMapDetail(
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
