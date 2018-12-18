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

            #region tamanio elementos

            viewBuscadorFondo.Frame = new CGRect(0, 20, View.Frame.Width, 50);


            imgIconoBuscador.Image = UIImage.FromFile("lupa_icon.png");
            imgIconoBuscador.Frame = new CGRect(20, 15, 20, 20);

            txtBuscador.Frame = new CGRect(50, 5, viewBuscadorFondo.Frame.Width - 50, 40);
            txtBuscador.Placeholder = "Buscar";
            txtBuscador.AttributedPlaceholder = new Foundation.NSAttributedString("Buscar", null, UIColor.FromRGB(255, 188, 188));
            txtBuscador.Layer.BorderColor = UIColor.Clear.CGColor;
            txtBuscador.ClipsToBounds = true;

            viewLinea.Frame = new CGRect(20, viewBuscadorFondo.Frame.Height - 10, viewBuscadorFondo.Frame.Width - 40, 1);

            tableCaballos.Frame = new CGRect(0, 70, View.Frame.Width, View.Frame.Height - 70);


            btnAgregar.Frame = new CGRect((View.Frame.Width / 2) - 30, (View.Frame.Height - 150), 60, 60);
            btnAgregar.Layer.CornerRadius = 30;

            progress.Frame = new CGRect((View.Frame.Width / 2) - (progress.Frame.Width / 2), (View.Frame.Height / 2) - (progress.Frame.Height / 2), progress.Frame.Width, progress.Frame.Height);
            progress.Hidden = true;

            #endregion

            #region ocultar teclado al tocar la pantalla;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);

            #endregion;

            #region consulta caballos 

            #region Actualizar caballo



            #endregion

            #endregion;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

