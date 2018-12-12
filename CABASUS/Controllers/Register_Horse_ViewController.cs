using System;
using System.Text.RegularExpressions;
using CoreGraphics;
using UIKit;

namespace CABASUS.Controllers
{
    public partial class Register_Horse_ViewController : UIViewController
    {
        public Register_Horse_ViewController() : base("Register_Horse_ViewController", null)
        {
        }

        protected Register_Horse_ViewController(IntPtr handle) : base(handle){}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            #region Medidas y ubicacion de objetos en la interfaz 

            btn_back.Frame = new CGRect(0, 35, 50, 20);

            btn_foto.Frame = new CGRect((View.Frame.Width / 2) - 50, 0, 100, 100);
            btn_foto.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            btn_foto.Layer.BorderWidth = 1f;
            btn_foto.Layer.CornerRadius = 50f;
            btn_foto.ClipsToBounds = true;
            btn_foto.Tag = 1;

            lbl_NameHorse.Frame = new CGRect(25, 120, View.Frame.Width - 50, 30);
            txt_nameHorse.Frame = new CGRect(25, 155, View.Frame.Width - 50, 40);
            txt_nameHorse.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_nameHorse.Layer.BorderWidth = 1f;

            lbl_weight.Frame = new CGRect(25, 205, View.Frame.Width - 50, 30);
            txt_weight.Frame = new CGRect(25, 240, View.Frame.Width - 50, 40);
            txt_weight.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_weight.Layer.BorderWidth = 1f;

            lbl_height.Frame = new CGRect(25, 290, View.Frame.Width - 50, 30);
            txt_height.Frame = new CGRect(25, 325, View.Frame.Width - 50, 40);
            txt_height.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_height.Layer.BorderWidth = 1f;
            txt_height.SecureTextEntry = true;

            lbl_breed.Frame = new CGRect(25, 375, View.Frame.Width - 50, 30);
            txt_breed.Frame = new CGRect(25, 410, View.Frame.Width - 50, 40);
            txt_breed.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_breed.Layer.BorderWidth = 1f;

            lbl_dob.Frame = new CGRect(25, 460, View.Frame.Width - 50, 30);
            txt_dob.Frame = new CGRect(25, 495, View.Frame.Width - 50, 40);
            txt_dob.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_dob.Layer.BorderWidth = 1f;

            lbl_gender.Frame = new CGRect(25, 545, View.Frame.Width - 50, 30);
            txt_gender.Frame = new CGRect(25, 580, View.Frame.Width - 50, 40);
            txt_gender.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_gender.Layer.BorderWidth = 1f;

            lbl_oat.Frame = new CGRect(25, 630, View.Frame.Width - 50, 30);
            txt_oat.Frame = new CGRect(25, 665, View.Frame.Width - 50, 40);
            txt_oat.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_oat.Layer.BorderWidth = 1f;

            btn_done.Frame = new CGRect(25, 770, View.Frame.Width - 50, 40);
            btn_done.Layer.CornerRadius = 20f;
            btn_done.ClipsToBounds = true;


            scroll.Frame = new CGRect(0, 55, View.Frame.Width, View.Frame.Height - 55);
            scroll.ContentSize = new CGSize(View.Frame.Width, 850);

            progress.Frame = new CGRect((View.Frame.Width / 2) - (progress.Frame.Width / 2), (View.Frame.Height / 2) - (progress.Frame.Height / 2), progress.Frame.Width, progress.Frame.Height);
            progress.Hidden = true;
            #endregion;

            #region ocultar teclado al tocar la pantalla;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);

            #endregion;

            #region mover pantalla cuando se este llenando el formulario;

            UIKeyboard.Notifications.ObserveWillShow((sender, args) =>
            {
                Action action = () =>
                {
                    scroll.ContentSize = new CGSize(View.Frame.Width, 850 + args.FrameEnd.Height);
                };

                UIViewPropertyAnimator animator = new UIViewPropertyAnimator(.3, UIViewAnimationCurve.Linear, action);
                animator.StartAnimation();

                if (txt_dob.IsEditing)
                {
                    scroll.SetContentOffset(new CGPoint(0, 150), true);
                }
            });


            UIKeyboard.Notifications.ObserveWillHide((sender, args) =>
            {
                Action action = () =>
                {
                    scroll.ContentSize = new CGSize(View.Frame.Width, 850);
                };

                UIViewPropertyAnimator animator = new UIViewPropertyAnimator(.3, UIViewAnimationCurve.Linear, action);
                animator.StartAnimation();
            });

            #endregion;

            #region abrir camara o galeria para la foto del ususario;

            btn_foto.TouchUpInside += delegate {
                GaleryCameraAccessController obj = new GaleryCameraAccessController();

                var okCancelAlertController = UIAlertController.Create(null, "Selecciona una opcion", UIAlertControllerStyle.Alert);

                okCancelAlertController.AddAction(UIAlertAction.Create("Galeria", UIAlertActionStyle.Default, alert => obj.BringUpPhotoGallery(btn_foto, 50)));
                okCancelAlertController.AddAction(UIAlertAction.Create("Camara", UIAlertActionStyle.Default, alert => obj.BringUpCamera(btn_foto, 50)));
                okCancelAlertController.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));

                PresentViewController(okCancelAlertController, true, null);
            };

            #endregion;

            #region comprobar que no se pegue nada en la fecha

            txt_dob.EditingChanged += delegate {

                var noLetras = txt_dob.Text.Replace("/", "");
                noLetras = noLetras.Replace(" ", "");

                try
                {
                    int.Parse(noLetras);
                    try
                    {
                        var separado = txt_dob.Text.Split('/');
                        if (separado.Length == 3 && new Regex("^[0-9]$").IsMatch(separado[2][1].ToString()))
                        {
                            Regex dia = new Regex("^[0-9]{1,2} $");
                            if (!dia.IsMatch(separado[0]))
                                txt_dob.Text = "00 /" + separado[1] + "/" + separado[2];
                            Regex mes = new Regex("^ [0-9]{1,2} $");
                            if (!mes.IsMatch(separado[1]))
                                txt_dob.Text = separado[0] + "/ 00 /" + separado[2];
                            Regex anio = new Regex("^ [0-9]{4}$");
                            if (anio.IsMatch(separado[3]))
                                txt_dob.Text = separado[0] + "/" + separado[1] + "/ 0000";
                        }
                    }
                    catch { }

                    try
                    {
                        if (txt_dob.Text[txt_dob.Text.Length - 1] == '/')
                        {
                            txt_dob.Text = txt_dob.Text.Substring(0, txt_dob.Text.Length - 4);
                        }

                    }
                    catch { }

                    if (txt_dob.Text.Length == 2)
                    {
                        txt_dob.Text += " / ";
                    }
                    if (txt_dob.Text.Length == 7)
                    {
                        txt_dob.Text += " / ";
                    }
                    if (txt_dob.Text.Length >= 15)
                    {
                        txt_dob.Text = txt_dob.Text.Substring(0, txt_dob.Text.Length - 1);
                    }
                }
                catch
                {
                    txt_dob.Text = "";
                }

            };

            #endregion;





        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

