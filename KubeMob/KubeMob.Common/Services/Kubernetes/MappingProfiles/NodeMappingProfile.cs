using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class NodeMappingProfile : Profile
    {
        public NodeMappingProfile()
        {
            this.CreateMap<k8s.Models.V1Node, ObjectSummary>()
                .ConstructUsing((n) => new ObjectSummary(
                    n.Metadata.Name,
                    n.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1Node, NodeDetail>()
                .ConstructUsing((n) =>
                {
                    List<string> labels = n.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<string> annotations = n.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                               new List<string>();

                    string creationTime = n.Metadata.CreationTimestamp.HasValue
                        ? $"{n.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    var addresses = n.Status.Addresses.Select(a => $"{a.Type}: {a.Address}");
                    var podCidr = n.Spec.PodCIDR;
                    var providerId = n.Spec.ProviderID;
                    var unschedulable = n.Spec.Unschedulable.GetValueOrDefault(false);

                    // TODO expand this.
                    var machineId = n.Status.NodeInfo;

                    return new NodeDetail(
                        n.Metadata.Name,
                        n.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime);
                });
        }
    }
}
