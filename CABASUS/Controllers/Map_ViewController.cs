using System;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;

namespace CABASUS.Controllers
{
    public partial class Map_ViewController : UIViewController
    {
        ShareInSide S = new ShareInSide();
        bool primeraVez = true;

        public Map_ViewController() : base("Map_ViewController", null)
        {
        }

        protected Map_ViewController(IntPtr handle) : base(handle) { }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (primeraVez && S.consulTabState() == "u")
            {
                await Task.Delay(500);
                primeraVez = false;
            }

            else if(primeraVez && S.consulTabState() == "d")
            {
                await Task.Delay(500);
                primeraVez = false;

                Action a = () =>
                {
                    View.Frame = new CGRect(0, 130, View.Frame.Width, View.Frame.Height - 130);
                };
                UIViewPropertyAnimator Animar = new UIViewPropertyAnimator(.5, UIViewAnimationCurve.EaseInOut, a);
                Animar.StartAnimation();
                S.saveTabState("d");
            }

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

