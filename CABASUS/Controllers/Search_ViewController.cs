using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CABASUS.Adapters;
using CoreGraphics;
using Newtonsoft.Json;
using UIKit;
using CABASUS.Modelos;
using System.IO;

namespace CABASUS.Controllers
{
    public partial class Search_ViewController : UIViewController
    {
        string IP = "192.168.1.74";
        string server = "";

        public Search_ViewController() : base("Search_ViewController", null)
        {
        }

        public Search_ViewController(IntPtr intPtr):base (intPtr){}

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            #region Disenio frames y server
            server = "http://" + IP + ":5001/api/";

            viewBuscadorFondo.Frame = new CGRect(0, 20, View.Frame.Width, 50);

            btnBack.Frame = new CGRect(-50, 15, 50, 20);

            imgIconoBuscadorFondo.Image = UIImage.FromFile("lupa_icon.png");
            imgIconoBuscadorFondo.Frame = new CGRect(20, 15, 20, 20);

            txtBuscar.Frame = new CGRect(50, 5, viewBuscadorFondo.Frame.Width - 50, 40);
            txtBuscar.Placeholder = "Buscar";
            txtBuscar.AttributedPlaceholder = new Foundation.NSAttributedString("Buscar", null, UIColor.FromRGB(255, 188, 188));
            txtBuscar.Layer.BorderColor = UIColor.Clear.CGColor;
            txtBuscar.ClipsToBounds = true;

            viewLinea.Frame = new CGRect(20, viewBuscadorFondo.Frame.Height - 10, viewBuscadorFondo.Frame.Width - 40, 1);

            txtBuscar.ResignFirstResponder();
            txtBuscar.BecomeFirstResponder();
            txtBuscar.InputAccessoryView = new CustomKeyboard(txtBuscar, null, null);

            collection.Frame = new CGRect(0, 70, View.Frame.Width, View.Frame.Height - 70);
            collection.RegisterClassForCell(typeof(Collection_Adapter_Celda), "celdaCollection");

            var flotLayout = new UICollectionViewFlowLayout()
            {
                ItemSize = new CGSize((float)UIScreen.MainScreen.Bounds.Size.Width / 3.0f, (float)UIScreen.MainScreen.Bounds.Size.Width / 3.0f),
                SectionInset = new UIEdgeInsets(0, 0, 0, 0),
                ScrollDirection = UICollectionViewScrollDirection.Vertical,
                MinimumInteritemSpacing = 0,
                MinimumLineSpacing = 0
            };

            collection.CollectionViewLayout.InvalidateLayout();
            collection.SetCollectionViewLayout(flotLayout, false);

            #endregion

            #region ocultar teclado al tocar la pantalla;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);

            #endregion;

            #region consulta caballos aleatorios

            string nserver = server + "caballo/caballosaleatorios";

            try
            {
                var cliente =  new HttpClient();
                cliente.Timeout = TimeSpan.FromSeconds(20);
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new ShareInSide().consultxmlToken().token);

                HttpResponseMessage respuesta = null;
                string content = "";

                respuesta = await cliente.GetAsync(nserver);

                content = await respuesta.Content.ReadAsStringAsync();

                respuesta.EnsureSuccessStatusCode();

                if (respuesta.IsSuccessStatusCode)
                {
                    var listaCaballo = JsonConvert.DeserializeObject<List<caballos>>(content);

                    collection.Source = new Collection_Adapter(listaCaballo, View.Frame.Width);
                }
                else
                    Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            #endregion;

            btnBack.TouchUpInside += delegate
            {
                //try
                //{
                //    var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                //    var directoryname = Path.Combine(documentsDirectory, "Temporal");
                //    new ShareInSide().DeleteDirectory(directoryname);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

                var detalle = this.Storyboard.InstantiateViewController("Tabs_ViewController") as Tabs_ViewController;
                detalle.index = 0;
                detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                this.PresentViewController(detalle, true, null);
            };

            txtBuscar.EditingChanged += async delegate
            {
                nserver = server + "caballo/buscarCaballos?refe=" + txtBuscar.Text;
                try
                {
                    var cliente = new HttpClient();
                    cliente.Timeout = TimeSpan.FromSeconds(20);
                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new ShareInSide().consultxmlToken().token);

                    HttpResponseMessage respuesta = null;
                    string content = "";
                    
                    respuesta = await cliente.GetAsync(nserver);

                    content = await respuesta.Content.ReadAsStringAsync();

                    respuesta.EnsureSuccessStatusCode();

                    if (respuesta.IsSuccessStatusCode)
                    {
                        var listaCaballo = JsonConvert.DeserializeObject<List<caballos>>(content);

                        collection.Source = new Collection_Adapter(listaCaballo, View.Frame.Width);
                        collection.ReloadData();
                    }
                    else
                        Console.WriteLine(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };

            Action action = () => {
                btnBack.Frame = new CGRect(0, 15, 50, 20);
                txtBuscar.Frame = new CGRect(85, 5, txtBuscar.Frame.Width - 55, 40);
                imgIconoBuscadorFondo.Frame = new CGRect(55, 15, 20, 20);
                viewLinea.Frame = new CGRect(55, viewBuscadorFondo.Frame.Height - 10, viewBuscadorFondo.Frame.Width - 75, 1);
            };


            UIViewPropertyAnimator animator = new UIViewPropertyAnimator(.3, UIViewAnimationCurve.Linear, action);
            //await Task.Delay(1);
            animator.StartAnimation();
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}