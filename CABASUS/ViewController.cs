using System;
using Pager;
using UIKit;
using CABASUS.Controllers;

namespace CABASUS
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public ViewController() : base("ViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var colors = new[]
            {
                UIColor.FromRGB(255,255,255),
                UIColor.FromRGB(255,255,255),
                UIColor.FromRGB(255,255,255),
                UIColor.FromRGB(255,255,255),
                UIColor.FromRGB(255,255,255),
            }; //lol

            var pages = new[]
            {   new Paginas() { Title = "First"  },
                new Paginas() { Title = "Second" },
                new Paginas() { Title = "Third" },
                new Paginas() { Title = "Fourth" },
                new Paginas() { Title = "Last" },
            };

            var style = new PagerStyle()
            {
                SelectedStripColors = colors,
                BarHeight=50,
            };

            var pager = new PagerViewController(style, 80, pages);

            var nav = new UINavigationController(pager);
            nav.NavigationBar.Translucent = false;

            var mediabar = (int)UIApplication.SharedApplication.StatusBarFrame.Height;
            pager.View.Frame = new CoreGraphics.CGRect(0, mediabar, View.Frame.Width, View.Frame.Height - mediabar);

            View.Add(pager.View);
        }
         
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}
