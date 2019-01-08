using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CABASUS.Modelos;
using System.Net.Http;
using Newtonsoft.Json;

namespace CABASUS.Adapters
{
    public class Collection_Adapter : UICollectionViewSource
    {
        string IP = "192.168.1.74";
        HttpClient cliente = new HttpClient();
        string Server;
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

        public override async void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            Server = "http://" + IP + ":5001/api/Compartir/registrar/" + caballos[indexPath.Row].id_caballo;
            cliente.Timeout = TimeSpan.FromSeconds(20);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new ShareInSide().consultxmlToken().token);
            var respuesta = await cliente.PostAsync(Server, new StringContent("", System.Text.Encoding.UTF8, "application/json"));
            var datos = await respuesta.Content.ReadAsStringAsync();

            if (respuesta.IsSuccessStatusCode)
            {
                new ShareInSide().Toast("si se compartio");
            }
            else
            {
                new ShareInSide().Toast("no se compartio");
            }

        }

    }
}
