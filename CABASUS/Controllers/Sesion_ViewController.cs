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

            #region Medidas y ubicacion de objetos en la interfaz
            //Iphone x =414
            //Iphone 6=375
            //Iphone 5s=320
            btnback.Frame = new CGRect(0, 35, 50, 20);

            lbl_login.Frame = new CGRect(0, 50, View.Frame.Width, 50);

            lblemail.Frame = new CGRect(25, 130, View.Frame.Width - 50, 30);
            txt_email.Frame = new CGRect(25, 165, View.Frame.Width - 50, 40);
            txt_email.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            txt_email.Layer.BorderWidth = 1f;

            lbl_password.Frame = new CGRect(25, 215, View.Frame.Width - 50, 30);
            txt_password.Frame = new CGRect(25, 250, View.Frame.Width - 50, 40);
            txt_password.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            txt_password.Layer.BorderWidth = 1f;

            btn_login.Frame = new CGRect(25, 315, View.Frame.Width - 50, 40);
            btn_login.Layer.CornerRadius = 20f;
            btn_login.ClipsToBounds = true;

            btn_recovery.Frame = new CGRect(25, 355, View.Frame.Width - 50, 50);
            btn_recovery.TitleLabel.Lines = 2;
            btn_recovery.TitleLabel.TextAlignment = UITextAlignment.Center;
            #endregion

            #region Accion para boton recovery
            btn_recovery.TouchUpInside += delegate
            {
                var contenedor = new UIView();
                if (View.Frame.Width==320)
                    contenedor.Frame = new CGRect(30, 120, View.Frame.Width - 60, View.Frame.Height - 240);
                else if (View.Frame.Width == 414)
                    contenedor.Frame = new CGRect(50, 250, View.Frame.Width - 100, View.Frame.Height - 500);
                else
                    contenedor.Frame = new CGRect(50, 150, View.Frame.Width - 100, View.Frame.Height - 300);

                var lbl_reset = new UILabel(new CGRect(0,20,contenedor.Frame.Width,20));
                lbl_reset.Text = "Reset your password";
                lbl_reset.TextAlignment = UITextAlignment.Center;

                var lbl_msjrecovery = new UILabel( new CGRect(15,45,contenedor.Frame.Width-30,100));
                lbl_msjrecovery.Text = "Enter the email address you used to register. We will send you a message with your password.";
                lbl_msjrecovery.Lines = 5;
                var lbl_emailrecovery = new UILabel(new CGRect(15,160,contenedor.Frame.Width-20,30));
                lbl_emailrecovery.Text = "Email";

                var txt_emailrecovery = new UILabel(new CGRect(15, 195, contenedor.Frame.Width - 30, 40));
                txt_emailrecovery.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
                txt_emailrecovery.Layer.BorderWidth = 1f;

                var btn_send = new UIButton(new CGRect(25, 270, contenedor.Frame.Width - 50, 40));
                btn_send.SetTitle("SEND",UIControlState.Normal);
                btn_send.Layer.CornerRadius = 20f;
                btn_send.ClipsToBounds = true;
                btn_send.BackgroundColor = UIColor.FromRGB(246,128,25);

                contenedor.AddSubviews(lbl_reset,lbl_msjrecovery,lbl_emailrecovery, txt_emailrecovery, btn_send);

                GenerarAlerta(View, contenedor);
            };
            #endregion

            btn_login.TouchUpInside+=delegate {

                var detalle = this.Storyboard.InstantiateViewController("ViewController") as ViewController;
                detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                this.PresentViewController(detalle, true, null);
            };
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public void GenerarAlerta( UIView view,UIView contenido)
        {
            UIButton alerta = new UIButton(new CGRect(View.Frame.Width, View.Frame.Height, 0, 0));
            alerta.BackgroundColor = new UIColor(0 / 255, 0 / 255, 0 / 255, 127.5f / 255);

            UIView  alerta1= new UIView(new CGRect(View.Frame.Width, View.Frame.Height, 0, 0));
            alerta1.BackgroundColor = UIColor.White;

            alerta.AddSubview(alerta1);
            view.AddSubview(alerta);

            Action a = () =>
            {
                alerta.Frame = new CGRect(0, 0, View.Frame.Width, View.Frame.Height);
                alerta1.Frame = contenido.Frame;

            };
            UIViewPropertyAnimator Animar = new UIViewPropertyAnimator(.3, UIViewAnimationCurve.EaseInOut, a);
            Animar.StartAnimation();

            contenido.Frame = new CGRect(0,0,alerta1.Frame.Width,alerta1.Frame.Height);
            alerta1.AddSubview(contenido);
            alerta.TouchUpInside += delegate {

                alerta.RemoveFromSuperview();
            };
        }
    }
}

