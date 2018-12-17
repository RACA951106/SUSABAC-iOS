using System;
using System.Collections.Generic;
using CABASUS.Modelos;
using Foundation;
using UIKit;

namespace CABASUS.Adapters
{
    public class Breed_Adapter : UITableViewSource
    {
        List<razas> _razas;
        UIButton _btnBreed;
        UIButton _view;

        public Breed_Adapter(List<razas> razas, UIButton btnBreed, UIButton view)
        {
            _razas = razas;
            _btnBreed = btnBreed;
            _view = view;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell(UITableViewCellStyle.Default, "");
            cell.TextLabel.Text = _razas[indexPath.Row].raza; 
            cell.Tag = _razas[indexPath.Row].id_raza;

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var selectedName = _razas[indexPath.Row].raza; 
            _btnBreed.SetTitle(selectedName, UIControlState.Normal); 
            _btnBreed.Tag = _razas[indexPath.Row].id_raza;
            _btnBreed.SetTitleColor(UIColor.Black, UIControlState.Normal);
            _view.RemoveFromSuperview();
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _razas.Count;
        }
    }
}
