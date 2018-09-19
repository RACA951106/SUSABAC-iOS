using System;

using UIKit;

namespace CABASUS
{
    public partial class Paginas : UIViewController
    {
        public Paginas() : base("Paginas", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}

