using System;
using UIKit;
using CoreGraphics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CABASUS.Controllers
{
    public partial class Sesion_ViewController : UIViewController
    {
        string ip = "192.168.1.74";
        HttpClient cliente = new HttpClient();
        string serverLogin;
        string serverConsulta;
        string serverRecovery;

        public Sesion_ViewController() : base("Sesion_ViewController", null)
        {
        }

        public Sesion_ViewController(IntPtr handle) : base(handle)
        { 
        } 

        public override void ViewDidLoad()
        {
            txt_email.Text = "a@a.com";
            txt_password.Text = "a";

            serverLogin = "http://" + ip + ":5001/api/Account/Login";
            serverConsulta = "http://"+ ip +":5001/api/Usuario/consultar";
            serverRecovery = "http://" + ip + ":5001/api/Account/recuperarPass";

            base.ViewDidLoad();
            string emailGuardado = "", emailGuardadoRecovery = "";
            Regex email = new Regex(@"^([0-9a-zA-Z]" + //Start with a digit or alphabetical
                                            @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continuous or ending +-_. chars in email
                                            @")+" +
                                            @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$");


            #region Medidas y ubicacion de objetos en la interfaz
            //Iphone x =414
            //Iphone 6=375
            //Iphone 5s=320

            btnback.Frame = new CGRect(0, 35, 50, 20);

            lbl_login.Frame = new CGRect(0, 50, View.Frame.Width, 50);

            lblemail.Frame = new CGRect(25, 130, View.Frame.Width - 50, 30);
            txt_email.Frame = new CGRect(25, 165, View.Frame.Width - 50, 40);
            txt_email.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_email.Layer.BorderWidth = 1f;
            txt_email.KeyboardType = UIKeyboardType.EmailAddress;

            lbl_password.Frame = new CGRect(25, 215, View.Frame.Width - 50, 30);
            txt_password.Frame = new CGRect(25, 250, View.Frame.Width - 50, 40);
            txt_password.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_password.Layer.BorderWidth = 1f;
            txt_password.SecureTextEntry = true;

            btn_login.Frame = new CGRect(25, 315, View.Frame.Width - 50, 40);
            btn_login.Layer.CornerRadius = 20f;
            btn_login.ClipsToBounds = true;

            btn_recovery.Frame = new CGRect(25, 355, View.Frame.Width - 50, 50);
            btn_recovery.TitleLabel.Lines = 2;
            btn_recovery.TitleLabel.TextAlignment = UITextAlignment.Center;

            progreso.Frame = new CGRect((View.Frame.Width / 2) - (progreso.Frame.Width / 2), (View.Frame.Height / 1.5) - (progreso.Frame.Height / 2), progreso.Frame.Width, progreso.Frame.Height);
            progreso.Hidden = true;
            #endregion

            #region Accion para boton recovery
            btn_recovery.TouchUpInside += delegate
            {
                btn_recovery.Enabled = false;

                var contenedor = new UIView();
                if (View.Frame.Width == 320)
                    contenedor.Frame = new CGRect(30, 120, View.Frame.Width - 60, View.Frame.Height - 240);
                else if (View.Frame.Width == 414)
                    contenedor.Frame = new CGRect(50, 250, View.Frame.Width - 100, View.Frame.Height - 500);
                else
                    contenedor.Frame = new CGRect(50, 150, View.Frame.Width - 100, View.Frame.Height - 300);

                var lbl_reset = new UILabel(new CGRect(0, 20, contenedor.Frame.Width, 20));
                lbl_reset.Text = "Reset your password";
                lbl_reset.TextAlignment = UITextAlignment.Center;

                var lbl_msjrecovery = new UILabel(new CGRect(15, 45, contenedor.Frame.Width - 30, 100));
                lbl_msjrecovery.Text = "Enter the email address you used to register. We will send you a message with your password.";
                lbl_msjrecovery.Lines = 5;
                var lbl_emailrecovery = new UILabel(new CGRect(15, 160, contenedor.Frame.Width - 20, 30));
                lbl_emailrecovery.Text = "Email";

                var txt_emailrecovery = new UITextField(new CGRect(15, 195, contenedor.Frame.Width - 30, 40));
                txt_emailrecovery.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
                txt_emailrecovery.Layer.BorderWidth = 1f;
                txt_emailrecovery.KeyboardType = UIKeyboardType.EmailAddress;

                var btn_send = new UIButton(new CGRect(25, 270, contenedor.Frame.Width - 50, 40));
                btn_send.SetTitle("SEND", UIControlState.Normal);
                btn_send.Layer.CornerRadius = 20f;
                btn_send.ClipsToBounds = true;
                btn_send.BackgroundColor = UIColor.FromRGB(203, 30, 30);

                contenedor.AddSubviews(lbl_reset, lbl_msjrecovery, lbl_emailrecovery, txt_emailrecovery, btn_send);

                txt_emailrecovery.EditingChanged += delegate {

                    if(!string.IsNullOrEmpty(txt_emailrecovery.Text))
                        emailGuardadoRecovery = txt_emailrecovery.Text;

                };



                txt_emailrecovery.EditingDidBegin += delegate
                {
                    txt_emailrecovery.Text = emailGuardadoRecovery;
                    txt_emailrecovery.BackgroundColor = UIColor.White;
                    emailGuardadoRecovery = txt_emailrecovery.Text;
                    txt_email.Placeholder = "";
                };

                btn_send.TouchUpInside += async delegate
                {
                    progreso.Frame = new CGRect((View.Frame.Width / 2) - (progreso.Frame.Width / 2), (View.Frame.Height / 2) - (progreso.Frame.Height / 2), progreso.Frame.Width, progreso.Frame.Height);

                    View.BringSubviewToFront(progreso);
                    progreso.StartAnimating();
                    progreso.Hidden = false; 

                    if (email.IsMatch(txt_emailrecovery.Text))
                    {
                        #region Consumo API Recovery Password
                        txt_emailrecovery.Enabled = false;
                        btn_send.Enabled = false;

                        cliente.Timeout = TimeSpan.FromSeconds(20);
                        serverRecovery += "?email=" + txt_emailrecovery.Text + "&idioma=" + 1;
                        var respuesta = await cliente.GetAsync(serverRecovery);
                        var datos = await respuesta.Content.ReadAsStringAsync();

                        if (respuesta.IsSuccessStatusCode)
                        {
                            txt_emailrecovery.Enabled = false;
                            btn_send.Enabled = false;

                            progreso.StopAnimating();
                            progreso.Hidden = true;

                            new ShareInSide().Toast("Te enviamos una contraseña de repuesto a tu correo, por favor revisala :)\n\r"
                                                    + "Si no recives ninguna contraseña, favor de intentar nuevamente");
                        }
                        #endregion
                    }
                    else
                    {

                        emailGuardadoRecovery = txt_emailrecovery.Text;
                        txt_emailrecovery.Text = "";
                        txt_emailrecovery.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                        txt_emailrecovery.Placeholder = "Formato no valido para email";
                    }

                    txt_emailrecovery.Enabled = true;
                    btn_send.Enabled = true;

                    progreso.StopAnimating();
                    progreso.Hidden = true;
                };

                GenerarAlerta(View, contenedor);


            };
            #endregion

            #region Cambiar de color EditText
            txt_email.EditingChanged += delegate
            {
                if (!string.IsNullOrEmpty(txt_email.Text))
                    emailGuardado = txt_email.Text;
            };
            txt_email.EditingDidBegin += delegate {
                txt_email.Text = emailGuardado;
                txt_email.BackgroundColor = UIColor.White;
                txt_email.Placeholder = "";
                emailGuardado = txt_email.Text;
            };

            txt_password.EditingDidBegin += delegate
            {
                txt_password.BackgroundColor = UIColor.White;
                txt_password.Placeholder = "";
            };

            #endregion

            #region ocultar teclado al tocar la pantalla;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);

            #endregion;

            btn_login.TouchUpInside += async delegate
            {
                try
                {
                    if(string.IsNullOrEmpty(txt_email.Text))
                    {
                        txt_email.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                        txt_email.Placeholder = "Llene este campo";
                    }
                    else 
                    {
                        if(string.IsNullOrEmpty(txt_password.Text))
                        {
                            txt_password.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                            txt_password.Placeholder = "Llene este campo";
                        }
                        else
                        {
                            if(email.IsMatch(txt_email.Text))
                            {
                                progreso.StartAnimating();
                                progreso.Hidden = false;

                                btn_login.Enabled = false;
                                btn_recovery.Enabled = false;

                                //Servidor APIS
                                var login = new Modelos.login
                                {
                                    usuario = txt_email.Text,
                                    contrasena = txt_password.Text,
                                    id_dispositivo = UIDevice.CurrentDevice.IdentifierForVendor.AsString(),
                                    SO = "iOS",
                                    TokenFB = new ShareInSide().consultxmlTokenFB().token
                                };

                                cliente.Timeout = TimeSpan.FromSeconds(20);
                                var data = JsonConvert.SerializeObject(login);
                                var respuesta = await cliente.PostAsync(serverLogin, new StringContent(data, System.Text.Encoding.UTF8, "application/json"));
                                var datos = await respuesta.Content.ReadAsStringAsync();

                                if (respuesta.IsSuccessStatusCode)
                                {
                                    //XML
                                    var contenido = JsonConvert.DeserializeObject<Modelos.tokens>(datos);
                                    new ShareInSide().savexmlToken(contenido.token, contenido.expiration);

                                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", new ShareInSide().consultxmlToken().token);
                                    var url = await cliente.GetAsync(serverConsulta);
                                    datos = await url.Content.ReadAsStringAsync();
                                    var usuario = JsonConvert.DeserializeObject<List<Modelos.usuarios>>(datos);

                                    usuario[0].contrasena = txt_password.Text;

                                    new ShareInSide().savexmlUsuario(usuario[0]);

                                    await new ShareInSide().DownloadImageAsync(usuario[0].foto);

                                    progreso.StopAnimating();
                                    progreso.Hidden = true;

                                    var detalle = this.Storyboard.InstantiateViewController("Hamburguer_ViewController") as Hamburguer_ViewController;
                                    //detalle.index = 0;
                                    detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                                    detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                                    this.PresentViewController(detalle, true, null);
                                }
                                else
                                {
                                    Console.WriteLine(datos);
                                    progreso.StopAnimating();
                                    progreso.Hidden = true;
                                    btn_recovery.Enabled = true;
                                }
                            }
                            else
                            {
                                emailGuardado = txt_email.Text;
                                txt_email.Text = "";
                                txt_email.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                                txt_email.Placeholder = "Formato no valido para email";

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    new ShareInSide().Toast(ex.Message);
                    progreso.StopAnimating();
                    progreso.Hidden = true;
                }
                btn_login.Enabled = true;
                btn_recovery.Enabled = true;
            };

            btnback.TouchUpInside+=delegate {

                var detalle = this.Storyboard.InstantiateViewController("Login_ViewController") as Login_ViewController;
                detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                this.PresentViewController(detalle, true, null);
            };
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public void GenerarAlerta(UIView view,UIView contenido)
        {
            UIButton alerta = new UIButton(new CGRect(View.Frame.Width, View.Frame.Height, 0, 0));
            alerta.BackgroundColor = new UIColor(0 / 255, 0 / 255, 0 / 255, 127.5f / 255);

            UIView  alerta1= new UIView(new CGRect(View.Frame.Width, View.Frame.Height, 0, 0));
            alerta1.BackgroundColor = UIColor.White;

            alerta.AddSubview(alerta1);
            view.AddSubview(alerta);

            Action a = () =>
            {
                alerta.Frame = new CGRect(0, 0, View.Frame.Width, View.Frame.Height);
                alerta1.Frame = contenido.Frame;

            };
            UIViewPropertyAnimator Animar = new UIViewPropertyAnimator(.3, UIViewAnimationCurve.EaseInOut, a);
            Animar.StartAnimation();

            contenido.Frame = new CGRect(0,0,alerta1.Frame.Width,alerta1.Frame.Height);
            alerta1.AddSubview(contenido);
            alerta.TouchUpInside += delegate {

                progreso.Frame = new CGRect((View.Frame.Width / 2) - (progreso.Frame.Width / 2), (View.Frame.Height / 1.5) - (progreso.Frame.Height / 2), progreso.Frame.Width, progreso.Frame.Height);

                alerta.RemoveFromSuperview();
                btn_recovery.Enabled = true;
            };
        }
    }
}

