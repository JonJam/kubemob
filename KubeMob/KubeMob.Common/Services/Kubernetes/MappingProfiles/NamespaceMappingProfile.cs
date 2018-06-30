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
            //TODO status
            this.CreateMap<k8s.Models.V1Namespace, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1Namespace, NamespaceDetail>()
                .ConstructUsing((n) =>
                {
                    List<string> labels = n.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<string> annotations = n.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                               new List<string>();

                    string creationTime = n.Metadata.CreationTimestamp.HasValue
                        ? $"{n.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    return new NamespaceDetail(
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
