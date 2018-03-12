namespace KubeMob.ViewModels
{
    public class MainViewModel : ExtendedBindableObject
    {
        private string text;

        public MainViewModel()
        {
            // TODO Remove
            this.Text = "Welcome to Xamarin.Forms!";
        }

        public string Text
        {
            get => this.text;
            set => this.SetProperty(ref this.text, value);
        }
    }
}
