using System;
using CoreGraphics;
using UIKit;

namespace CABASUS.Controllers
{
    public partial class Register_ViewController : UIViewController
    {
        public Register_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            #region Medidas y ubicacion de objetos en la interfaz 

            btnback.Frame = new CGRect(0, 35, 50, 20);

            btn_foto.Frame = new CGRect((View.Frame.Width / 2) - 50, 0, 100, 100);
            btn_foto.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            btn_foto.Layer.BorderWidth = 1f;
            btn_foto.Layer.CornerRadius = 50f;
            btn_foto.ClipsToBounds = true;

            lbl_username.Frame= new CGRect(25, 120, View.Frame.Width - 50, 30);
            txt_username.Frame = new CGRect(25, 155, View.Frame.Width - 50, 40);
            txt_username.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            txt_username.Layer.BorderWidth = 1f;

            lbl_email.Frame= new CGRect(25,205, View.Frame.Width - 50, 30);
            txt_email.Frame= new CGRect(25, 240, View.Frame.Width - 50, 40);
            txt_email.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            txt_email.Layer.BorderWidth = 1f;

            lbl_pw.Frame = new CGRect(25, 290, View.Frame.Width - 50, 30);
            txt_pw.Frame = new CGRect(25, 325, View.Frame.Width - 50, 40);
            txt_pw.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            txt_pw.Layer.BorderWidth = 1f;

            lbl_dob.Frame = new CGRect(25, 375, View.Frame.Width - 50, 30);
            txt_dob.Frame = new CGRect(25, 410, View.Frame.Width - 50, 40);
            txt_dob.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            txt_dob.Layer.BorderWidth = 1f;

            lbl_phone.Frame = new CGRect(25, 460, View.Frame.Width - 50, 30);
            txt_phone.Frame = new CGRect(25, 495, View.Frame.Width - 50, 40);
            txt_phone.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            txt_phone.Layer.BorderWidth = 1f;

            btn_done.Frame = new CGRect(25, 550, View.Frame.Width - 50, 40);
            btn_done.Layer.CornerRadius = 20f;
            btn_done.ClipsToBounds = true;

            btn_terms.Frame = new CGRect(25, 595, View.Frame.Width - 50, 60);
            btn_terms.TitleLabel.Lines = 3;
            btn_terms.TitleLabel.TextAlignment=UITextAlignment.Center;

            scroll_register.Frame = new CGRect(0, 35, View.Frame.Width, View.Frame.Height - 35);
            scroll_register.ContentSize= new CGSize(View.Frame.Width, 670);
            #endregion
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}

