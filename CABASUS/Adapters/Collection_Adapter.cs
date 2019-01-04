using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CABASUS.Modelos;

namespace CABASUS.Adapters
{
    public class Collection_Adapter : UICollectionViewSource
    {
        List<caballos> caballos;
        double tview;

        public Collection_Adapter(List<caballos> _caballos, double _tview)
        {
            caballos = _caballos;
            tview = _tview;
        }
        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return caballos.Count;
        }

        public override bool ShouldHighlightItem(UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (Collection_Adapter_Celda)collectionView.CellForItem(indexPath);
            cell.lblNombre.Alpha = .05f;
        }

        public override void ItemUnhighlighted(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (Collection_Adapter_Celda)collectionView.CellForItem(indexPath);
            cell.lblNombre.Alpha = 1f;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (Collection_Adapter_Celda)collectionView.DequeueReusableCell("celdaCollection", indexPath);
            cell.updateCell(caballos[indexPath.Row]);
            return cell;
        }

        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            //UIAlertView uIAlertView = new UIAlertView()
            //{
            //    Message = "Hola " + indexPath.Row
            //};
            //uIAlertView.AddButton("OK");
            //uIAlertView.Show();
        }

    }
}
