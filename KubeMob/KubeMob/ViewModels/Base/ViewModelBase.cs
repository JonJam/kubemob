using System.Threading.Tasks;

namespace KubeMob.Common.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        public virtual Task Initialize(object navigationData) => Task.CompletedTask;
    }
}
