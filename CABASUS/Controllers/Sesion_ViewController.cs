using System;
using UIKit;
using CoreGraphics;

namespace CABASUS.Controllers
{
    public partial class Sesion_ViewController : UIViewController
    {
        public Sesion_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            lbl_login.Frame = new CGRect(0, 50, View.Frame.Width, 50);


        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}

