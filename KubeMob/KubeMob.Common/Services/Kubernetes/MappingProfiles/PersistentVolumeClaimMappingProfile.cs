using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class PersistentVolumeClaimMappingProfile : Profile
    {
        public PersistentVolumeClaimMappingProfile()
        {
            this.CreateMap<k8s.Models.V1PersistentVolumeClaim, ObjectSummary>()
                .ConstructUsing((p) =>
                {
                    Status status = Status.None;

                    switch (p.Status.Phase)
                    {
                        case "Bound":
                            status = Status.Success;
                            break;
                        case "Pending":
                            status = Status.Pending;
                            break;
                        case "Lost":
                            status = Status.Error;
                            break;
                        default:
                            break;
                    }

                    return new ObjectSummary(
                        p.Metadata.Name,
                        p.Metadata.NamespaceProperty,
                        status);
                });

            this.CreateMap<k8s.Models.V1PersistentVolumeClaim, PersistentVolumeClaimDetail>()
                .ConstructUsing((p) =>
                {
                    List<MetadataItem> labels = p.Metadata.Labels?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                new List<MetadataItem>();
                    List<MetadataItem> annotations = p.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                     new List<MetadataItem>();

                    string creationTime = p.Metadata.CreationTimestamp.HasValue
                        ? $"{p.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<Capacity> capacity = p.Status.Capacity?.Select(kvp => new Capacity(kvp.Key, kvp.Value.ToString())).ToList() ??
                                              new List<Capacity>();

                    return new PersistentVolumeClaimDetail(
                        p.Metadata.Uid,
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
