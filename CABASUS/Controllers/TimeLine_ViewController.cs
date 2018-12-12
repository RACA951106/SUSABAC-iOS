﻿using System;
using System.IO;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;

namespace CABASUS.Controllers
{
    public partial class TimeLine_ViewController : UIViewController
    {
        ShareInSide S = new ShareInSide();
        public TimeLine_ViewController() : base("TimeLine_ViewController", null)
        {
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            await Task.Delay(2000);

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

        protected TimeLine_ViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}

