using KubeMob.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(DisclosureIndicatorViewCellRenderer))]
namespace KubeMob.iOS.Renderers
{
    /// <summary>
    /// Based off <see cref="https://montemagno.com/adding-a-disclosure-indicator-accessory-to/"/>
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DisclosureIndicatorViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(
            Cell item,
            UITableViewCell reusableCell,
            UITableView tv)
        {
            UITableViewCell cell = base.GetCell(item, reusableCell, tv);

            switch (item.StyleId)
            {
                case nameof(UITableViewCellAccessory.Checkmark):
                    cell.Accessory = UITableViewCellAccessory.Checkmark;
                    break;
                case nameof(UITableViewCellAccessory.DetailButton):
                    cell.Accessory = UITableViewCellAccessory.DetailButton;
                    break;
                case nameof(UITableViewCellAccessory.DetailDisclosureButton):
                    cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
                    break;
                case nameof(UITableViewCellAccessory.DisclosureIndicator):
                    cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                    break;
                default:
                    cell.Accessory = UITableViewCellAccessory.None;
                    break;
            }

            var view = new UIView();
            view.BackgroundColor = UIColor.Red;
            cell.AccessoryView = view;

            return cell;
        }
    }
}