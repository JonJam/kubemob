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
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1beta1Ingress, IngressDetail>()
                .ConstructUsing((s) =>
                {
                    List<string> labels = s.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<string> annotations = s.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                               new List<string>();

                    string creationTime = s.Metadata.CreationTimestamp.HasValue
                        ? $"{s.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    return new IngressDetail(
                        s.Metadata.Name,
                        s.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime);
                });
        }
    }
}
