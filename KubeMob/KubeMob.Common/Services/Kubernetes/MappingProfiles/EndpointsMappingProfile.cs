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
            this.CreateMap<k8s.Models.V1Endpoints, EndpointDetail>()
                .ConstructUsing((e) =>
                {
                    IEnumerable<EndpointAddress> addresses = e.Subsets.SelectMany(s =>
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

                        IEnumerable<EndpointAddress> readyAddresses =
                            s.Addresses?.SelectMany(a =>
                                ports.Select(p => new EndpointAddress(a.Hostname, p, a.NodeName, true))) ??
                            new List<EndpointAddress>();

                        IEnumerable<EndpointAddress> notReadyAddresses = s.NotReadyAddresses?.SelectMany(a => ports.Select(p => new EndpointAddress(a.Ip, p, a.NodeName, false))) ??
                                                                         new List<EndpointAddress>();

                        List<EndpointAddress> subsetAddresses = new List<EndpointAddress>();

                        subsetAddresses.AddRange(readyAddresses);
                        subsetAddresses.AddRange(notReadyAddresses);

                        return subsetAddresses;
                    });

                    return new EndpointDetail(
                        addresses.ToList().AsReadOnly());
                });
        }
    }
}
