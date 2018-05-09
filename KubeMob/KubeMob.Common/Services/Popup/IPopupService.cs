using System.Threading.Tasks;

namespace KubeMob.Common.Services.Popup
{
    public interface IPopupService
    {
        Task<bool> DisplayAlert(
            string title,
            string message,
            string acceptButtonText,
            string cancelButtonText);

        Task DisplayAlert(
            string title,
            string message,
            string acceptButtonText);
    }
}
