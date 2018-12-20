using System;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;

namespace CABASUS.Controllers
{
    public partial class Tabs_ViewController : UITabBarController
    {
        public int index = 1;
        public Tabs_ViewController() : base("Tabs_ViewController", null)
        {
        }

        protected Tabs_ViewController(IntPtr Handle) : base(Handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            UIView newView = new UIView();

            newView.Frame = new CGRect(0, 20, View.Frame.Width, 110);
            newView.BackgroundColor = UIColor.Red;

            View.Add(newView);
            View.SendSubviewToBack(newView);

            SelectedIndex = index;

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}

