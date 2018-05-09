using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Popup
{
    public class PopupService : IPopupService
    {
        [Preserve]
        public PopupService()
        {
        }

        public Task<bool> DisplayAlert(
            string title,
            string message,
            string acceptButtonText,
            string cancelButtonText)
        {
            Page currentPage = Application.Current.MainPage;

            return currentPage.DisplayAlert(
                title,
                message,
                acceptButtonText,
                cancelButtonText);
        }

        public Task DisplayAlert(
            string title,
            string message,
            string acceptButtonText)
        {
            Page currentPage = Application.Current.MainPage;

            return currentPage.DisplayAlert(
                title,
                message,
                acceptButtonText);
        }
    }
}
