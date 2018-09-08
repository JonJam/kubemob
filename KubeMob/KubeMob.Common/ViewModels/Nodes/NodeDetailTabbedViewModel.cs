using KubeMob.Common.Services.Kubernetes.Model;
using KubeMob.Common.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Nodes
{
    [Preserve(AllMembers = true)]
    public class NodeDetailTabbedViewModel
        : ObjectDetailTabbedViewModelBase<NodeDetailViewModel, NodeDetail>
    {
        public NodeDetailTabbedViewModel(
            NodeDetailViewModel detailVm)
            : base(detailVm)
        {
        }
    }
}
