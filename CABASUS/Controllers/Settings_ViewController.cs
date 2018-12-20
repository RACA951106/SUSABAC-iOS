using System;
using CABASUS.Adapters;
using UIKit;

namespace CABASUS.Controllers
{
    public partial class Settings_ViewController : UIViewController
    {
        public Settings_ViewController(IntPtr handle) : base(handle)
        {

        }

        public Settings_ViewController() : base("Settings_ViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            listaSettings.Frame = new CoreGraphics.CGRect(0, 20, View.Frame.Width, View.Frame.Height);
            listaSettings.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            listaSettings.ScrollEnabled = false;
            listaSettings.Source = new Settings_Adapter(this);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

