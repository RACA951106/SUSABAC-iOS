using System;
using System.Collections.Generic;
using CABASUS.Adapters;
using CoreGraphics;
using Foundation;
using UIKit;

namespace CABASUS.Controllers
{
    public partial class Chat_Controller : UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        List<string> mensajes = new List<string>();
        public string idChat = "";
        public string nombreCaballo = "";

        public Chat_Controller() : base("Chat_Controller", null)
        {
        }

        public Chat_Controller(IntPtr intPtr):base(intPtr){}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            for (int i = 0; i < 80; i++)
            {
                mensajes.Add("Hola ! " + i);
            }

            #region Frames

            tableChat.Frame = new CGRect(0, 70, View.Frame.Width, View.Frame.Height - 150);

            txtMensaje.Frame = new CGRect(20, View.Frame.Height - 60, View.Frame.Width - 100, 40);
            txtMensaje.Layer.CornerRadius = 10;
            txtMensaje.Layer.BorderWidth = 1;
            txtMensaje.Layer.MasksToBounds = true;
            btnEnviarMensaje.Frame = new CGRect(txtMensaje.Frame.Width + 10, txtMensaje.Frame.Y, View.Frame.Width - (txtMensaje.Frame.Width) - 30, 40);
            btnEnviarMensaje.SetTitle("Enviar", UIControlState.Normal);

            viewFondo.Frame = new CGRect(0, 20, View.Frame.Width, 50);
            btnBack.Frame = new CGRect(0, 15, 50, 20);
            lblNombre.Frame = new CGRect(0, 0, View.Frame.Width, 50);
            lblNombre.TextAlignment = UITextAlignment.Center;
            lblNombre.TextColor = UIColor.White;
            lblNombre.Font = lblNombre.Font.WithSize(20);

            lblNombre.Text = nombreCaballo;



            #endregion

            #region ocultar teclado al tocar la pantalla;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);
             
            #endregion;  
             
            #region mover pantalla cuando se este llenando el formulario;

            UIKeyboard.Notifications.ObserveWillShow(async (sender, args) =>
            {
                //Action action = () =>
                //{
                View.Frame = new CGRect(0, - args.FrameEnd.Height, View.Frame.Width, View.Frame.Height);
                //};

                //UIViewPropertyAnimator animator = new UIViewPropertyAnimator(.3, UIViewAnimationCurve.Linear, action);
                //animator.StartAnimation();


            });

            UIKeyboard.Notifications.ObserveWillHide((sender, args) =>
            {
                //Action action = () =>
                //{
                //    scroll.ContentSize = new CGSize(View.Frame.Width, 850);
                //};

                //UIViewPropertyAnimator animator = new UIViewPropertyAnimator(.3, UIViewAnimationCurve.Linear, action);
                //animator.StartAnimation();
            });

            #endregion;

            tableChat.WeakDelegate = this;
            tableChat.WeakDataSource = this;

            btnBack.TouchUpInside += delegate {

                var detalle = Storyboard.InstantiateViewController("Tabs_ViewController") as Tabs_ViewController;
                detalle.index = 3;
                detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                this.PresentViewController(detalle, true, null);
            };
            Tablehastaabajo();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return mensajes.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (Custom_Cell_Chat_Adapter)tableChat.DequeueReusableCell("celdaChat", indexPath);
            cell.ActualizarDatos(mensajes[indexPath.Row], indexPath.Row);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
              
            return cell;
        }

        public void Tablehastaabajo()
        {
            var lastSectionIndex = tableChat.NumberOfSections() - 1;
            var lastRowIndex = tableChat.NumberOfRowsInSection(lastSectionIndex) - 1;
            var pathToLastRow = NSIndexPath.FromRowSection(lastRowIndex, lastSectionIndex);
            tableChat.ScrollToRow(pathToLastRow, UITableViewScrollPosition.None, true);
        }
    }
}

