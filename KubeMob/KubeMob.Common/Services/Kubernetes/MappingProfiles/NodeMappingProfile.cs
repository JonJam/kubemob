using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using k8s.Models;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class NodeMappingProfile : Profile
    {
        public NodeMappingProfile()
        {
            this.CreateMap<V1Node, ObjectSummary>()
                .ConstructUsing((n) =>
                {
                    Status status = Status.Unknown;

                    V1NodeCondition cond = n.Status.Conditions.FirstOrDefault(c => c.Type == "Ready");

                    if (cond != null)
                    {
                        switch (cond.Status)
                        {
                            case "True":
                                status = Status.Success;
                                break;
                            case "False":
                                status = Status.Error;
                                break;
                            default:
                                break;
                        }
                    }

                    return new ObjectSummary(
                        n.Metadata.Name,
                        n.Metadata.NamespaceProperty,
                        status);
                });

            this.CreateMap<V1NodeCondition, Condition>()
                .ConstructUsing((c) =>
                {
                    string lastHeartbeatTime = c.LastHeartbeatTime.HasValue
                        ? $"{c.LastHeartbeatTime.Value.ToUniversalTime():s} UTC"
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

            this.CreateMap<V1Node, NodeDetail>()
                .ConstructUsing((n) =>
                {
                    List<MetadataItem> labels = n.Metadata.Labels?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                new List<MetadataItem>();
                    List<MetadataItem> annotations = n.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                     new List<MetadataItem>();

                    string creationTime = n.Metadata.CreationTimestamp.HasValue
                        ? $"{n.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<string> addresses = n.Status.Addresses.Select(a => $"{a.Type}: {a.Address}").ToList();

                    List<Condition> conditions = Mapper.Map<List<Condition>>(n.Status.Conditions);

                    return new NodeDetail(
                        n.Metadata.Uid,
                        n.Metadata.Name,
                        n.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        addresses.AsReadOnly(),
                        n.Spec.PodCIDR,
                        n.Spec.ProviderID,
                        n.Spec.Unschedulable.GetValueOrDefault(false),
                        n.Status.NodeInfo.MachineID,
                        n.Status.NodeInfo.SystemUUID,
                        n.Status.NodeInfo.BootID,
                        n.Status.NodeInfo.KernelVersion,
                        n.Status.NodeInfo.OsImage,
                        n.Status.NodeInfo.ContainerRuntimeVersion,
                        n.Status.NodeInfo.KubeletVersion,
                        n.Status.NodeInfo.KubeProxyVersion,
                        n.Status.NodeInfo.OperatingSystem,
                        n.Status.NodeInfo.Architecture,
                        conditions.AsReadOnly());
                });
        }
    }
}
