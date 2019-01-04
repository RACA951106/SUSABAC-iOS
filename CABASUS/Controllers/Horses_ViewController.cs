using System;
using CoreGraphics;
using UIKit;
using CABASUS.Adapters;
using System.Net.Http;
using Newtonsoft.Json;
using CABASUS.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CABASUS.Controllers
{
    public partial class Horses_ViewController : UIViewController
    {
        ShareInSide S = new ShareInSide();
        string ip = "192.168.0.20";
        HttpClient cliente = new HttpClient();
        string serverHorses;
        UIRefreshControl refreshControl;

        public Horses_ViewController() : base("Horses_ViewController", null)
        {
        }

        protected Horses_ViewController(IntPtr handle) : base(handle) { }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (S.consulTabState() == "d")
            {
                View.Frame = new CGRect(0, 130, View.Frame.Width, View.Frame.Height - 130);

                Action a = () =>
                {
                    View.Frame = new CGRect(0, 0, View.Frame.Width, View.Frame.Height);
                };
                UIViewPropertyAnimator Animar = new UIViewPropertyAnimator(.5, UIViewAnimationCurve.EaseInOut, a);
                Animar.StartAnimation();
                S.saveTabState("u");
            }

            S.saveTabState("u");

            await ConsultarCaballos();
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            S.saveTabState("u");
            serverHorses = "http://" + ip + ":5001/api/caballo/consultaridusuario";

            #region activar el refresh;

            refreshControl = new UIRefreshControl();
            refreshControl.TintColor = UIColor.FromRGB(215, 51, 39);
            refreshControl.ValueChanged += async delegate
            {
                refreshControl.BeginRefreshing();
                await ConsultarCaballos();
                refreshControl.EndRefreshing();
            };

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                tableCaballos.RefreshControl = refreshControl;
            else
                tableCaballos.AddSubview(refreshControl);

            #endregion;

            txtBuscador.EditingDidBegin += delegate {
                var detalle = this.Storyboard.InstantiateViewController("Search_ViewController") as Search_ViewController;
                detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                this.PresentViewController(detalle, true, null);
            };

            #region tamanio elementos

            viewBuscadorFondo.Frame = new CGRect(0, 20, View.Frame.Width, 50);


            imgIconoBuscador.Image = UIImage.FromFile("lupa_icon.png");
            imgIconoBuscador.Frame = new CGRect(20, 15, 20, 20);

            txtBuscador.Frame = new CGRect(50, 5, viewBuscadorFondo.Frame.Width - 50, 40);
            txtBuscador.Placeholder = "Buscar";
            txtBuscador.AttributedPlaceholder = new Foundation.NSAttributedString("Buscar", null, UIColor.FromRGB(255, 188, 188));
            txtBuscador.Layer.BorderColor = UIColor.Clear.CGColor;
            txtBuscador.ClipsToBounds = true;

            viewLinea.Frame = new CGRect(20, viewBuscadorFondo.Frame.Height - 10, viewBuscadorFondo.Frame.Width - 40, 1);

            tableCaballos.Frame = new CGRect(0, 70, View.Frame.Width, View.Frame.Height - 70);


            btnAgregar.Frame = new CGRect((View.Frame.Width / 2) - 30, (View.Frame.Height - 150), 60, 60);

            UIImageView imgAdd = new UIImageView(new CGRect((btnAgregar.Frame.Width / 2) - 15, (btnAgregar.Frame.Height / 2) - 15, 30, 30));
            imgAdd.Image = UIImage.FromFile("anadir").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            imgAdd.TintColor = UIColor.White;

            btnAgregar.AddSubview(imgAdd);
            btnAgregar.Layer.CornerRadius = 30;


            #endregion

            #region ocultar teclado al tocar la pantalla;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);

            #endregion;

            #region consulatar caballos de la nube 

            await ConsultarCaballos();

            #endregion;

            btnAgregar.TouchUpInside += delegate {
                var detalle = this.Storyboard.InstantiateViewController("Register_Horse_ViewController") as Register_Horse_ViewController;
                detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                this.PresentViewController(detalle, true, null);
            };
        }

        public async Task<bool> ConsultarCaballos()
        {
            refreshControl.BeginRefreshing();

            try
            {
                cliente.Timeout = TimeSpan.FromSeconds(20);
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new ShareInSide().consultxmlToken().token);
                var respuesta = await cliente.GetAsync(serverHorses);
                var datos = await respuesta.Content.ReadAsStringAsync();

                if (respuesta.IsSuccessStatusCode)
                {
                    var listaCaballos = JsonConvert.DeserializeObject<List<caballos>>(datos);

                    #region agragar acciones al table view en swipe;

                    tableCaballos.Source = new Hoses_Adapter(listaCaballos);
                    tableCaballos.ReloadData();
                    tableCaballos.Delegate = new Horses_Adapter_Delegate(this, listaCaballos);

                    #endregion;
                }
                else
                    S.Toast(datos);
            }
            catch (Exception ex)
            {
                S.Toast(ex.Message);
            }

            refreshControl.EndRefreshing();

            return true;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

