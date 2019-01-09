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

            tableChat.Frame = new CGRect(0, 20, View.Frame.Width, View.Frame.Height - 100);

            txtMensaje.Frame = new CGRect(20, View.Frame.Height - 60, View.Frame.Width - 100, 40);
            txtMensaje.Layer.CornerRadius = 10;
            txtMensaje.Layer.MasksToBounds = true;
            btnEnviarMensaje.Frame = new CGRect(txtMensaje.Frame.Width + 10, txtMensaje.Frame.Y, View.Frame.Width - (txtMensaje.Frame.Width) - 30, 40);
            btnEnviarMensaje.SetTitle("Enviar", UIControlState.Normal);

            #endregion

            #region ocultar teclado al tocar la pantalla;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);

            #endregion;

            tableChat.WeakDelegate = this;
            tableChat.WeakDataSource = this;

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
    }
}

