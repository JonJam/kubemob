using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<k8s.Models.V1Service, ObjectSummary>()
                .ConstructUsing((s) =>
                {
                    Status status = Status.Success;

                    bool? hasExternalEndpoints = s.Status.LoadBalancer.Ingress?.Any();

                    if (string.IsNullOrWhiteSpace(s.Spec.ClusterIP) ||
                        (s.Spec.Type == "LoadBalancer" &&
                         !hasExternalEndpoints.GetValueOrDefault(false)))
                    {
                        status = Status.Pending;
                    }

                    return new ObjectSummary(
                        s.Metadata.Name,
                        s.Metadata.NamespaceProperty,
                        status);
                });

            this.CreateMap<k8s.Models.V1Service, ServiceDetail>()
                .ConstructUsing((s) =>
                {
                    List<string> labels = s.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<MetadataItem> annotations = s.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                     new List<MetadataItem>();

                    List<string> labelSelectors = s.Spec.Selector?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                                  new List<string>();

                    string creationTime = s.Metadata.CreationTimestamp.HasValue
                        ? $"{s.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<string> internalEndpoints = new List<string>();
                    List<string> externalEndpoints = new List<string>();

                    s.Spec.Ports.ForEach(p =>
                    {
                        internalEndpoints.Add($"{s.Metadata.Name}:{p.Port} {p.Protocol}");
                        internalEndpoints.Add($"{s.Metadata.Name}:{p.NodePort.GetValueOrDefault(0)} {p.Protocol}");

                        s.Status.LoadBalancer.Ingress?.ForEach(i =>
                        {
                            externalEndpoints.Add($"{i.Ip}:{p.Port}");
                        });
                    });

                    string selector = s.Spec.Selector != null
                        ? string.Join(",", s.Spec.Selector.Select(kvp => $"{kvp.Key}={kvp.Value}"))
                        : null;

                    return new ServiceDetail(
                        s.Metadata.Uid,
                        s.Metadata.Name,
                        s.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        labelSelectors.AsReadOnly(),
                        s.Spec.Type,
                        s.Spec.SessionAffinity,
                        s.Spec.ClusterIP,
                        internalEndpoints.AsReadOnly(),
                        externalEndpoints.AsReadOnly(),
                        selector);
                });
        }
    }
}
