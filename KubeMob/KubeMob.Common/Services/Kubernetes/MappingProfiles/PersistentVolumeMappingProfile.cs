using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class PersistentVolumeMappingProfile : Profile
    {
        public PersistentVolumeMappingProfile()
        {
            this.CreateMap<k8s.Models.V1PersistentVolume, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty,
                    r.Status.Phase));

            this.CreateMap<k8s.Models.V1PersistentVolume, PersistentVolumeDetail>()
                .ConstructUsing((p) =>
                {
                    List<string> labels = p.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<string> annotations = p.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                               new List<string>();

                    string creationTime = p.Metadata.CreationTimestamp.HasValue
                        ? $"{p.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    string claim = $"{p.Spec.ClaimRef.NamespaceProperty}/{p.Spec.ClaimRef.Name}";

                    List<Capacity> capacity = p.Spec.Capacity?.Select(kvp => new Capacity(kvp.Key, kvp.Value.ToString())).ToList() ??
                                              new List<Capacity>();

                    return new PersistentVolumeDetail(
                        p.Metadata.Name,
                        p.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        p.Status.Phase,
                        claim,
                        p.Spec.PersistentVolumeReclaimPolicy,
                        p.Spec.AccessModes.ToList().AsReadOnly(),
                        p.Spec.StorageClassName,
                        p.Status.Reason,
                        p.Status.Message,
                        capacity.AsReadOnly());
                });
        }
    }
}
