using System.Collections.Generic;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class NodeDetail : ObjectDetailBase
    {
        public NodeDetail(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<MetadataItem> labels,
            IReadOnlyList<MetadataItem> annotations,
            string creationTime,
            IReadOnlyList<string> addresses,
            string podCidr,
            string providerId,
            bool unschedulable,
            string machineId,
            string systemUuid,
            string bootId,
            string kernelVersion,
            string osImage,
            string containerRuntimeVersion,
            string kubeletVersion,
            string kubeProxyVersion,
            string operatingSystem,
            string architecture,
            IReadOnlyList<Condition> conditions)
            : base(uid, name, namespaceName, labels, annotations, creationTime)
        {
            this.Addresses = addresses;
            this.PodCidr = podCidr;
            this.ProviderId = providerId;
            this.Unschedulable = unschedulable;
            this.MachineId = machineId;
            this.SystemUuid = systemUuid;
            this.BootId = bootId;
            this.KernelVersion = kernelVersion;
            this.OsImage = osImage;
            this.ContainerRuntimeVersion = containerRuntimeVersion;
            this.KubeletVersion = kubeletVersion;
            this.KubeProxyVersion = kubeProxyVersion;
            this.OperatingSystem = operatingSystem;
            this.Architecture = architecture;
            this.Conditions = conditions;
        }

        public IReadOnlyList<string> Addresses
        {
            get;
        }

        public string PodCidr
        {
            get;
        }

        public string ProviderId
        {
            get;
        }

        public bool Unschedulable
        {
            get;
        }

        public string MachineId
        {
            get;
        }

        public string SystemUuid
        {
            get;
        }

        public string BootId
        {
            get;
        }

        public string KernelVersion
        {
            get;
        }

        public string OsImage
        {
            get;
        }

        public string ContainerRuntimeVersion
        {
            get;
        }

        public string KubeletVersion
        {
            get;
        }

        public string KubeProxyVersion
        {
            get;
        }

        public string OperatingSystem
        {
            get;
        }

        public string Architecture
        {
            get;
        }

        public IReadOnlyList<Condition> Conditions
        {
            get;
        }
    }
}
