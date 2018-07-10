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
                .ConstructUsing((p) =>
                {
                    Status status = Status.Unknown;

                    switch (p.Status.Phase)
                    {
                        case "Available":
                        case "Bound":
                            status = Status.Success;
                            break;
                        case "Pending":
                            status = Status.Update;
                            break;
                        case "Released":
                            status = Status.Warning;
                            break;
                        case "Failed":
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

            this.CreateMap<k8s.Models.V1PersistentVolume, PersistentVolumeDetail>()
                .ConstructUsing((p) =>
                {
                    List<string> labels = p.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<MetadataItem> annotations = p.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                     new List<MetadataItem>();

                    string creationTime = p.Metadata.CreationTimestamp.HasValue
                        ? $"{p.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    ObjectReference claim = Mapper.Map<ObjectReference>(p.Spec.ClaimRef);

                    List<Capacity> capacity = p.Spec.Capacity?.Select(kvp => new Capacity(kvp.Key, kvp.Value.ToString())).ToList() ??
                                              new List<Capacity>();

                    return new PersistentVolumeDetail(
                        p.Metadata.Uid,
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
