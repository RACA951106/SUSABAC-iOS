using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CABASUS.Adapters;
using CABASUS.Modelos;
using CoreGraphics;
using Newtonsoft.Json;
using UIKit;

namespace CABASUS.Controllers
{
    public partial class Notifications_ViewController : UIViewController
    {
        string IP = "192.168.1.74";
        string Server = "";
        HttpClient cliente = new HttpClient();

        ShareInSide S = new ShareInSide();
        public Notifications_ViewController() : base("Notifications_ViewController", null)
        {
        }

        protected Notifications_ViewController(IntPtr handle) : base(handle) { }

        public override void ViewWillAppear(bool animated)
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
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            #region Frames

            viewFondoBuscador.Frame = new CGRect(0, 20, View.Frame.Width, 50);

            imgLupa.Image = UIImage.FromFile("lupa_icon.png");
            imgLupa.Frame = new CGRect(20, 15, 20, 20);

            txtBuscador.Frame = new CGRect(50, 5, viewFondoBuscador.Frame.Width - 50, 40);
            txtBuscador.Placeholder = "Buscar";
            txtBuscador.AttributedPlaceholder = new Foundation.NSAttributedString("Buscar", null, UIColor.FromRGB(255, 188, 188));
            txtBuscador.Layer.BorderColor = UIColor.Clear.CGColor;
            txtBuscador.ClipsToBounds = true;

            viewLineaBuscador.Frame = new CGRect(20, viewFondoBuscador.Frame.Height - 10, viewFondoBuscador.Frame.Width - 40, 1);

            txtBuscador.ResignFirstResponder();
            txtBuscador.BecomeFirstResponder();
            txtBuscador.InputAccessoryView = new CustomKeyboard(txtBuscador, null, null);

            tableChat.Frame = new CGRect(0, 70, View.Frame.Width, View.Frame.Height - 119);

            #endregion

            #region ocultar teclado al tocar la pantalla;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);

            #endregion;

            #region Consulta compartidos

            await ConsultarCaballos();

            #endregion

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public async Task<bool> ConsultarCaballos()
        {
            //refreshControl.BeginRefreshing();

            try
            {
                Server = "http://" + IP + ":5001/api/compartir/consultarcompartidos";

                cliente.Timeout = TimeSpan.FromSeconds(20);
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new ShareInSide().consultxmlToken().token);
                var respuesta = await cliente.GetAsync(Server);
                var datos = await respuesta.Content.ReadAsStringAsync();

                if (respuesta.IsSuccessStatusCode)
                {
                    var listaCaballos = JsonConvert.DeserializeObject<List<compartidos>>(datos);

                    #region agragar acciones al table view en swipe;

                    tableChat.Source = new Horse_Share_Adapter(listaCaballos, this);
                    tableChat.ReloadData();
                    //tableChat.Delegate = new Horses_Adapter_Delegate(this, listaCaballos);

                    #endregion;
                }
                else
                    S.Toast(datos);
            }
            catch (Exception ex)
            {
                S.Toast(ex.Message);
            }

            //refreshControl.EndRefreshing();

            return true;
        }
    }
}

