// https://gist.github.com/jessejiang0214/63b29b3166330c6fc083
using ProMama.iOS.Components;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(iOSTransparentViewCellRenderer))]
namespace ProMama.iOS.Components
{
    public class iOSTransparentViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            if (cell != null)
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }
    }
}