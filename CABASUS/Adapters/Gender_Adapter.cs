using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace CABASUS.Adapters
{
    public class Gender_Adapter : UITableViewSource
    {
        UIButton _gender, _view;
        List<string> listGender = new List<string>() { "Filly", "Gelding", "Mare", "Stallion" };
        
        public Gender_Adapter(UIButton gender, UIButton view)
        {
            _gender = gender;
            _view = view;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell(UITableViewCellStyle.Default, "");
            cell.TextLabel.Text = listGender[indexPath.Row];
            cell.Tag = indexPath.Row + 1;

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var selectedName = listGender[indexPath.Row];
            _gender.SetTitle(selectedName, UIControlState.Normal);
            _gender.Tag = indexPath.Row + 1;

            _gender.SetTitleColor(UIColor.Black, UIControlState.Normal);
            _view.RemoveFromSuperview();
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return listGender.Count;
        }
    }
}
