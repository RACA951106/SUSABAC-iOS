using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CABASUS.Modelos;
using CoreGraphics;
using CABASUS.Controllers;

namespace CABASUS.Adapters
{
    public class Horse_Share_Adapter : UITableViewSource
    {
        UIViewController controlador;
        List<compartidos> listHorses;

        public Horse_Share_Adapter(List<compartidos> caballos, UIViewController _controlador)
        {
            listHorses = caballos;
            controlador = _controlador;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell(UITableViewCellStyle.Default, "");
            cell.TextLabel.Text = listHorses[indexPath.Row].nombre_caballo;
            cell.Tag = indexPath.Row;

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return listHorses.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var detalle = controlador.Storyboard.InstantiateViewController("Chat_Controller") as Chat_Controller;
            detalle.idChat = listHorses[indexPath.Row].id_caballo;
            detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
            detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
            controlador.PresentViewController(detalle, true, null);
        }
    }
}
