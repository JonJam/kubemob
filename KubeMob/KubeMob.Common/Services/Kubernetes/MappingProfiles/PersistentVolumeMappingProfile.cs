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

                    var status = p.Status.Phase;
                    // TODO Handle link
                    var claim = $"{p.Spec.ClaimRef.NamespaceProperty}/{p.Spec.ClaimRef.Name}";
                    var reclaimPolicy = p.Spec.PersistentVolumeReclaimPolicy;
                    var accessModes = string.Join(", ", p.Spec.AccessModes);
                    // TODO Handle link
                    var storageClass = p.Spec.StorageClassName;

                    // TODO add isvisible binding.
                    var reason = p.Status.Reason;
                    var message = p.Status.Message;

                    // TODO capacity
                    p.Spec.Capacity;

                    return new PersistentVolumeDetail(
                        p.Metadata.Name,
                        p.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime);
                });
        }
    }
}
