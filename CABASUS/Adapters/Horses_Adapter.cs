using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CABASUS.Modelos;

namespace CABASUS.Adapters
{
    public class Hoses_Adapter : UITableViewSource
    {
        List<caballos> listHorses;

        public Hoses_Adapter(List<caballos> caballos)
        {
            listHorses = caballos;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell(UITableViewCellStyle.Default, "");
            cell.TextLabel.Text = listHorses[indexPath.Row].nombre;

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return listHorses.Count;
        }
    }
}
