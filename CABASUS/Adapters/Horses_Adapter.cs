using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace CABASUS.Adapters
{
    public class Hoses_Adapter : UITableViewSource
    {
        List<string> listGender = new List<string>() { "Filly", "Gelding", "Mare", "Stallion" };

        public Hoses_Adapter()
        {
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell(UITableViewCellStyle.Default, "");
            cell.TextLabel.Text = listGender[indexPath.Row];
            cell.Tag = indexPath.Row + 1;

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return listGender.Count;
        }
    }
}
