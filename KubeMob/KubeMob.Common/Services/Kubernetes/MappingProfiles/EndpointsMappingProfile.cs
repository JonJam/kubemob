using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class EndpointsMappingProfile : Profile
    {
        public EndpointsMappingProfile()
        {
            this.CreateMap<k8s.Models.V1Endpoints, ObjectSummary>()
                .ConstructUsing((c) => new ObjectSummary(
                    c.Metadata.Name,
                    c.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1Endpoints, EndpointDetail>()
                .ConstructUsing((e) =>
                {
                    IEnumerable<EndpointAddress> addresses = e.Subsets?.SelectMany(s =>
                    {
                        IEnumerable<string> ports = s.Ports.Select(ep =>
                        {
                            List<string> parts = new List<string>();

                            if (!string.IsNullOrWhiteSpace(ep.Name))
                            {
                                parts.Add(ep.Name);
                            }

                            parts.Add(ep.Port.ToString());
                            parts.Add(ep.Protocol);

                            return string.Join(", ", parts);
                        });

                        string combinedPorts = string.Join(Environment.NewLine, ports);

                        IEnumerable<EndpointAddress> readyAddresses =
                            s.Addresses?.Select(a => new EndpointAddress(a.Ip, combinedPorts, a.NodeName, true)) ??
                            new List<EndpointAddress>();
                        IEnumerable<EndpointAddress> notReadyAddresses =
                            s.NotReadyAddresses?.Select(a => new EndpointAddress(a.Ip, combinedPorts, a.NodeName, false)) ??
                            new List<EndpointAddress>();

                        List<EndpointAddress> subsetAddresses = new List<EndpointAddress>();
                        subsetAddresses.AddRange(readyAddresses);
                        subsetAddresses.AddRange(notReadyAddresses);

                        return subsetAddresses;
                    }) ?? new List<EndpointAddress>();

                    return new EndpointDetail(
                        addresses.ToList().AsReadOnly());
                });
        }
    }
}
