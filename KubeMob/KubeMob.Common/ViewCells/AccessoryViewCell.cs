using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.ViewCells
{
    [Preserve(AllMembers = true)]
    public class AccessoryViewCell : ViewCell
    {
        public static readonly BindableProperty AccessoryProperty = BindableProperty.CreateAttached(
            nameof(AccessoryViewCell.AccessoryProperty),
            typeof(string),
            typeof(AccessoryViewCell),
            string.Empty);

        public string Accessory
        {
            get => (string)this.GetValue(AccessoryViewCell.AccessoryProperty);
            set => this.SetValue(AccessoryViewCell.AccessoryProperty, value);
        }
    }
}
