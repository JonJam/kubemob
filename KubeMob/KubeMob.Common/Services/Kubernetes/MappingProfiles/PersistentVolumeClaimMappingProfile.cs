using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class PersistentVolumeClaimMappingProfile : Profile
    {
        public PersistentVolumeClaimMappingProfile()
        {
            this.CreateMap<k8s.Models.V1PersistentVolumeClaim, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty,
                    r.Status.Phase));

            this.CreateMap<k8s.Models.V1PersistentVolumeClaim, PersistentVolumeClaimDetail>()
                .ConstructUsing((p) =>
                {
                    List<string> labels = p.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<string> annotations = p.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                               new List<string>();

                    string creationTime = p.Metadata.CreationTimestamp.HasValue
                        ? $"{p.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<Capacity> capacity = p.Status.Capacity?.Select(kvp => new Capacity(kvp.Key, kvp.Value.ToString())).ToList() ??
                                              new List<Capacity>();

                    return new PersistentVolumeClaimDetail(
                        p.Metadata.Name,
                        p.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        p.Status.Phase,
                        p.Spec.VolumeName,
                        p.Spec.AccessModes.ToList().AsReadOnly(),
                        p.Spec.StorageClassName,
                        capacity.AsReadOnly());
                });
        }
    }
}
