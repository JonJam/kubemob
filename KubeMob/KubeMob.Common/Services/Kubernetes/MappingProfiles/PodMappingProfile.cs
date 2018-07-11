using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class PodMappingProfile : Profile
    {
        public PodMappingProfile()
        {
            this.CreateMap<k8s.Models.V1Pod, ObjectSummary>()
                 .ConstructUsing((p) =>
                {
                    Status status = Status.None;

                    switch (p.Status.Phase)
                    {
                        case "Pending":
                            status = Status.Pending;
                            break;
                        case "Running":
                        case "Succeeded":
                            status = Status.Success;
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

            this.CreateMap<k8s.Models.V1EnvVar, EnvironmentVariable>()
                .ConstructUsing((e) => new EnvironmentVariable(
                    e.Name,
                    e.Value));

            this.CreateMap<k8s.Models.V1Container, Container>()
                .ConstructUsing((c) =>
                {
                    List<EnvironmentVariable> envVars = c.Env != null ? Mapper.Map<List<EnvironmentVariable>>(c.Env) : new List<EnvironmentVariable>();
                    List<string> commands = c.Command?.ToList() ?? new List<string>();
                    List<string> args = c.Args?.ToList() ?? new List<string>();

                    return new Container(
                        c.Name,
                        c.Image,
                        envVars.AsReadOnly(),
                        commands.AsReadOnly(),
                        args.AsReadOnly());
                });

            this.CreateMap<k8s.Models.V1PodCondition, Condition>()
                .ConstructUsing((c) =>
                {
                    string lastHeartbeatTime = c.LastProbeTime.HasValue
                        ? $"{c.LastProbeTime.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    string lastTransitionTime = c.LastTransitionTime.HasValue
                        ? $"{c.LastTransitionTime.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    return new Condition(
                        c.Type,
                        c.Status,
                        lastHeartbeatTime,
                        lastTransitionTime,
                        c.Reason,
                        c.Message);
                });

            this.CreateMap<k8s.Models.V1Pod, PodDetail>()
                .ConstructUsing((p) =>
                {
                    string creationTime = p.Metadata.CreationTimestamp.HasValue
                        ? $"{p.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<MetadataItem> labels = p.Metadata.Labels?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                new List<MetadataItem>();
                    List<MetadataItem> annotations = p.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                     new List<MetadataItem>();

                    List<Container> containers = Mapper.Map<List<Container>>(p.Spec.Containers);
                    List<Condition> conditions = Mapper.Map<List<Condition>>(p.Status.Conditions);
                    List<ObjectReference> owners = Mapper.Map<List<ObjectReference>>(p.Metadata.OwnerReferences);
                    List<string> pvcs = p.Spec.Volumes
                        .Where(v => v.PersistentVolumeClaim != null)
                        .Select(v => v.PersistentVolumeClaim.ClaimName).ToList();

                    return new PodDetail(
                        p.Metadata.Uid,
                        p.Metadata.Name,
                        p.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        p.Status.Phase,
                        p.Status.QosClass,
                        p.Spec.NodeName,
                        p.Status.PodIP,
                        containers.AsReadOnly(),
                        conditions.AsReadOnly(),
                        owners.AsReadOnly(),
                        pvcs.AsReadOnly());
                });
        }
    }
}
