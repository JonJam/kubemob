using KubeMob.Attributes;

namespace KubeMob.ViewModels
{
    [Preserve(AllMembers = true)]
    public class MainViewModel : ExtendedBindableObject
    {
        private string text;

        public MainViewModel()
        {
            this.Text = "Welcome to Xamarin.Forms!";
        }

        public string Text
        {
            get => this.text;
            set => this.SetProperty(ref this.text, value);
        }
    }
}
