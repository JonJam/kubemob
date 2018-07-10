using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class IngressMappingProfile : Profile
    {
        public IngressMappingProfile()
        {
            this.CreateMap<k8s.Models.V1beta1Ingress, ObjectSummary>()
                .ConstructUsing((i) => new ObjectSummary(
                    i.Metadata.Name,
                    i.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1beta1Ingress, IngressDetail>()
                .ConstructUsing((i) =>
                {
                    List<string> labels = i.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<MetadataItem> annotations = i.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                     new List<MetadataItem>();

                    string creationTime = i.Metadata.CreationTimestamp.HasValue
                        ? $"{i.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    return new IngressDetail(
                        i.Metadata.Uid,
                        i.Metadata.Name,
                        i.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime);
                });
        }
    }
}
