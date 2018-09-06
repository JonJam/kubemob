using FFImageLoading.Forms;
using Xamarin.Forms;

namespace KubeMob.Common.Pages.Base
{
    public partial class ExtendedContentPage : ContentPage
    {
        public static readonly BindableProperty IconImageProperty = BindableProperty.CreateAttached(
            nameof(ExtendedContentPage.IconImage),
            typeof(CachedImage),
            typeof(ExtendedContentPage),
            null,
            propertyChanged: ExtendedContentPage.OnIconImageChanged);

        public ExtendedContentPage() => this.InitializeComponent();

        public CachedImage IconImage
        {
            get => (CachedImage)this.GetValue(ExtendedContentPage.IconImageProperty);
            set => this.SetValue(ExtendedContentPage.IconImageProperty, value);
        }

        private static void OnIconImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ExtendedContentPage page = (ExtendedContentPage)bindable;

            FileImageSource imageSource = null;

            if (page.IconImage != null &&
                page.IconImage.Source is FileImageSource fileImageSource)
            {
                imageSource = fileImageSource;
            }

            page.Icon = imageSource;
        }
    }
}