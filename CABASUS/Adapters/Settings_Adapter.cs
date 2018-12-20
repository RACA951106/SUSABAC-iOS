using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;
using CABASUS.Controllers;

namespace CABASUS.Adapters
{
    public class Settings_Adapter: UITableViewSource
    {
        UIViewController controller;
        public Settings_Adapter(UIViewController controlador)
        {
            controller = controlador;
        }

        List<string> opciones = new List<string>() { "Account", "About", "Terms and Conditions", "Log out" };

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell(UITableViewCellStyle.Default, "");
            cell.Tag = indexPath.Row + 1;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.Frame = new CGRect(0, 0, cell.Frame.Width, 60);

            UIView lineaI = new UIView(new CGRect(0, 5, 3, cell.Frame.Height - 5));
            lineaI.BackgroundColor = UIColor.Red;

            UILabel titulo = new UILabel(new CGRect(6, 0, cell.Frame.Width - 12, cell.Frame.Height));
            titulo.Text = opciones[indexPath.Row];

            cell.AddSubviews(lineaI, titulo);

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            switch(indexPath.Row)
            {
                case 0:
                    var detalle = controller.Storyboard.InstantiateViewController("Register_ViewController") as Register_ViewController;
                    detalle.indicadorAccion = false;
                    detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                    detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                    controller.PresentViewController(detalle, true, null);
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    new ShareInSide().Toastquestion("realmente quieres salir?", "settings", controller);
                    break;
            }
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 60;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return opciones.Count;
        }
    }
}
