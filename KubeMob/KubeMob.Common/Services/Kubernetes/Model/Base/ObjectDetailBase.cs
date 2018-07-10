using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model.Base
{
    [Preserve(AllMembers = true)]
    public abstract class ObjectDetailBase
    {
        protected ObjectDetailBase(
            string uid,
            string name,
            string namespaceName,
            IReadOnlyList<string> labels,
            IReadOnlyList<MetadataItem> annotations,
            string creationTime)
        {
            this.Uid = uid;
            this.Name = name;
            this.NamespaceName = namespaceName;
            this.Labels = labels;
            this.Annotations = annotations;
            this.CreationTime = creationTime;
        }

        public string Uid
        {
            get;
        }

        public string Name
        {
            get;
        }

        public string NamespaceName
        {
            get;
        }

        public IReadOnlyList<string> Labels
        {
            get;
        }

        // TODO Handle links
        public IReadOnlyList<MetadataItem> Annotations
        {
            get;
        }

        public string CreationTime
        {
            get;
        }
    }
}
