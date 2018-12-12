using System;
using CoreGraphics;
using UIKit;

namespace CABASUS.Controllers
{
    public partial class Horses_ViewController : UIViewController
    {
        ShareInSide S = new ShareInSide();
        public Horses_ViewController() : base("Horses_ViewController", null)
        {
        }

        protected Horses_ViewController(IntPtr handle) : base(handle) { }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (S.consulTabState() == "d")
            {
                View.Frame = new CGRect(0, 130, View.Frame.Width, View.Frame.Height - 130);

                Action a = () =>
                {
                    View.Frame = new CGRect(0, 0, View.Frame.Width, View.Frame.Height);
                };
                UIViewPropertyAnimator Animar = new UIViewPropertyAnimator(.5, UIViewAnimationCurve.EaseInOut, a);
                Animar.StartAnimation();
                S.saveTabState("u");
            }

            S.saveTabState("u");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            S.saveTabState("u");
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

