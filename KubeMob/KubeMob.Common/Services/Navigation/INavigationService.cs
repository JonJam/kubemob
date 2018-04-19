using System.Threading.Tasks;

namespace KubeMob.Common.Services.Navigation
{
    /// <summary>
    /// Based off <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/navigation#navigating-between-pages"/>
    /// </summary>
    public interface INavigationService
    {
        Task Initialize();

        Task NavigateToAddAccountPage();

        Task NavigateToAddEditAzureAccountPage(string id = null);

        Task NavigateToClusterPage();

        Task NavigateToPodsPage();

        Task NavigateToPodDetailPage();

        Task RemoveLastFromBackStack();

        Task RemoveBackStack();

        Task GoBackToClusterPage();
    }
}
