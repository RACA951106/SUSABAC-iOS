using System;
using CoreGraphics;
using UIKit;

namespace CABASUS.Controllers
{
    public partial class Map_ViewController : UIViewController
    {
        ShareInSide S = new ShareInSide();
        public Map_ViewController() : base("Map_ViewController", null)
        {
        }

        protected Map_ViewController(IntPtr handle) : base(handle) { }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (S.consulTabState() == "u")
            {
                Action a = () =>
                {
                    View.Frame = new CGRect(0, 130, View.Frame.Width, View.Frame.Height - 130);
                };
                UIViewPropertyAnimator Animar = new UIViewPropertyAnimator(.5, UIViewAnimationCurve.EaseInOut, a);
                Animar.StartAnimation();
                S.saveTabState("d");
            }
            else
            {
                View.Frame = new CGRect(0, 130, View.Frame.Width, View.Frame.Height - 130);
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

