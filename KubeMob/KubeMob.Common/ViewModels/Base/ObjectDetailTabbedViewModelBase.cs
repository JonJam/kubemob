using System.Threading.Tasks;
using KubeMob.Common.Services.Kubernetes.Model.Base;
using KubeMob.Common.Services.Navigation;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewModels.Base
{
    [Preserve(AllMembers = true)]
    public abstract class ObjectDetailTabbedViewModelBase<T1, T2> : ViewModelBase
        where T1 : ObjectDetailViewModelBase<T2>
        where T2 : ObjectDetailBase
    {
        private ObjectId objectId;

        protected ObjectDetailTabbedViewModelBase(
            T1 detailVm) => this.DetailVm = detailVm;

        public ObjectId ObjectId
        {
            get => this.objectId;
            private set => this.SetProperty(ref this.objectId, value);
        }

        public T1 DetailVm
        {
            get;
        }

        public override Task Initialize(object navigationData)
        {
            this.ObjectId = (ObjectId)navigationData;

            return this.DetailVm.Initialize(navigationData);
        }
    }
}
