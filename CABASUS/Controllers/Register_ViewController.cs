using System;
using System.Net.Http;
using System.Text;
using CoreGraphics;
using Foundation;
using Photos;
using UIKit;
using CABASUS.Modelos;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using ObjCRuntime;

namespace CABASUS.Controllers
{
    public partial class Register_ViewController : UIViewController
    {
        public Register_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            #region Medidas y ubicacion de objetos en la interfaz 

            btnback.Frame = new CGRect(0, 35, 50, 20);

            btn_foto.Frame = new CGRect((View.Frame.Width / 2) - 50, 0, 100, 100);
            btn_foto.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            btn_foto.Layer.BorderWidth = 1f;
            btn_foto.Layer.CornerRadius = 50f;
            btn_foto.ClipsToBounds = true;
            btn_foto.Tag = 1;

            lbl_username.Frame = new CGRect(25, 120, View.Frame.Width - 50, 30);
            txt_username.Frame = new CGRect(25, 155, View.Frame.Width - 50, 40);
            txt_username.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_username.Layer.BorderWidth = 1f;

            lbl_email.Frame = new CGRect(25, 205, View.Frame.Width - 50, 30);
            txt_email.Frame = new CGRect(25, 240, View.Frame.Width - 50, 40);
            txt_email.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_email.Layer.BorderWidth = 1f;

            lbl_pw.Frame = new CGRect(25, 290, View.Frame.Width - 50, 30);
            txt_pw.Frame = new CGRect(25, 325, View.Frame.Width - 50, 40);
            txt_pw.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_pw.Layer.BorderWidth = 1f;
            txt_pw.SecureTextEntry = true;

            lbl_dob.Frame = new CGRect(25, 375, View.Frame.Width - 50, 30);
            txt_dob.Frame = new CGRect(25, 410, View.Frame.Width - 50, 40);
            txt_dob.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_dob.Layer.BorderWidth = 1f;

            btn_done.Frame = new CGRect(25, 550, View.Frame.Width - 50, 40);
            btn_done.Layer.CornerRadius = 20f;
            btn_done.ClipsToBounds = true;

            btn_terms.Frame = new CGRect(25, 595, View.Frame.Width - 50, 60);
            btn_terms.TitleLabel.Lines = 3;
            btn_terms.TitleLabel.TextAlignment = UITextAlignment.Center;

            scroll_register.Frame = new CGRect(0, 55, View.Frame.Width, View.Frame.Height - 55);
            scroll_register.ContentSize = new CGSize(View.Frame.Width, 670);

            progreso.Frame = new CGRect((View.Frame.Width / 2) - (progreso.Frame.Width / 2), (View.Frame.Height / 2) - (progreso.Frame.Height / 2), progreso.Frame.Width, progreso.Frame.Height);
            progreso.Hidden = true;
            #endregion;

            #region ocultar teclado al tocar la pantalla;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);

            #endregion;

            #region mover pantalla cuando se este llenando el formulario;

            UIKeyboard.Notifications.ObserveWillShow((sender, args) =>
            {
                Action action = () =>
                {
                    scroll_register.ContentSize = new CGSize(View.Frame.Width, 670 + args.FrameEnd.Height);
                };

                UIViewPropertyAnimator animator = new UIViewPropertyAnimator(.3, UIViewAnimationCurve.Linear, action);
                animator.StartAnimation();

                if (txt_dob.IsEditing)
                {
                    scroll_register.SetContentOffset(new CGPoint(0, 150), true);
                }
            });


            UIKeyboard.Notifications.ObserveWillHide((sender, args) =>
            {
                Action action = () =>
                {
                    scroll_register.ContentSize = new CGSize(View.Frame.Width, 670);
                };

                UIViewPropertyAnimator animator = new UIViewPropertyAnimator(.3, UIViewAnimationCurve.Linear, action);
                animator.StartAnimation();
            });

            #endregion;

            #region abrir camara o galeria para la foto del ususario;

            btn_foto.TouchUpInside += delegate {
                GaleryCameraAccessController obj = new GaleryCameraAccessController();

                var okCancelAlertController = UIAlertController.Create(null, "Selecciona una opcion", UIAlertControllerStyle.Alert);

                okCancelAlertController.AddAction(UIAlertAction.Create("Galeria", UIAlertActionStyle.Default, alert => obj.BringUpPhotoGallery(btn_foto, 50)));
                okCancelAlertController.AddAction(UIAlertAction.Create("Camara", UIAlertActionStyle.Default, alert => obj.BringUpCamera(btn_foto, 50)));
                okCancelAlertController.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));

                PresentViewController(okCancelAlertController, true, null);
            };

            #endregion;

            #region comprobar que no se pegue nada en la fecha

            txt_dob.EditingChanged += delegate {

                var noLetras = txt_dob.Text.Replace("/", "");
                noLetras = noLetras.Replace(" ", "");

                try
                {
                    int.Parse(noLetras);
                    try
                    {
                        var separado = txt_dob.Text.Split('/');
                        if (separado.Length == 3 && new Regex("^[0-9]$").IsMatch(separado[2][1].ToString()))
                        {
                            Regex dia = new Regex("^[0-9]{1,2} $");
                            if (!dia.IsMatch(separado[0]))
                                txt_dob.Text = "00 /" + separado[1] + "/" + separado[2];
                            Regex mes = new Regex("^ [0-9]{1,2} $");
                            if (!mes.IsMatch(separado[1]))
                                txt_dob.Text = separado[0] + "/ 00 /" + separado[2];
                            Regex anio = new Regex("^ [0-9]{4}$");
                            if (anio.IsMatch(separado[3]))
                                txt_dob.Text = separado[0] + "/" + separado[1] + "/ 0000";
                        }
                    }
                    catch { }

                    try
                    {
                        if (txt_dob.Text[txt_dob.Text.Length - 1] == '/')
                        {
                            txt_dob.Text = txt_dob.Text.Substring(0, txt_dob.Text.Length - 4);
                        }

                    }
                    catch { }

                    if (txt_dob.Text.Length == 2)
                    {
                        txt_dob.Text += " / ";
                    }
                    if (txt_dob.Text.Length == 7)
                    {
                        txt_dob.Text += " / ";
                    }
                    if (txt_dob.Text.Length >= 15)
                    {
                        txt_dob.Text = txt_dob.Text.Substring(0, txt_dob.Text.Length - 1);
                    }
                }
                catch
                {
                    txt_dob.Text = "";
                }

            };

            #endregion;

            #region guardar datos al precionar boton aceptar;

            btn_done.TouchUpInside += async delegate
            {
                btn_done.Enabled = false;
                btn_foto.Enabled = false;

                #region comproba campos del formulario;

                Regex exregEmail = new Regex(@"^([0-9a-zA-Z]" + //Start with a digit or alphabetical
                                           @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continuous or ending +-_. chars in email
                                           @")+" +
                                           @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$");

                bool usuario = false, email = false, pass = false;
                string guardarEmail = "";

                if (string.IsNullOrEmpty(txt_username.Text))
                {
                    txt_username.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                    txt_username.Placeholder = "llene este campo";
                    usuario = false;
                }
                else
                    usuario = true;
                if (string.IsNullOrEmpty(txt_email.Text))
                {
                    txt_email.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                    txt_email.Placeholder = "llene este campo";
                    email = false;
                }
                else if (!exregEmail.IsMatch(txt_email.Text))
                {
                    guardarEmail = txt_email.Text;
                    txt_email.Text = "";
                    txt_email.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                    txt_email.Placeholder = "correo invalido";
                    email = false;
                }
                else
                    email = true;
                if (string.IsNullOrEmpty(txt_pw.Text))
                {
                    txt_pw.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                    txt_pw.Placeholder = "llene este campo";
                    pass = false;
                }
                else
                    pass = true;

                //cambiar cuando vuelva a editar
                txt_username.EditingDidBegin += delegate {

                    txt_username.BackgroundColor = UIColor.White;
                };
                txt_email.EditingDidBegin += delegate {

                    txt_email.Text = guardarEmail;
                    txt_email.BackgroundColor = UIColor.White;
                };
                txt_pw.EditingDidBegin += delegate
                {
                    txt_pw.BackgroundColor = UIColor.White;
                };

                //comprobar si la fecha es correcta en caso de que este llena

                if (!string.IsNullOrEmpty(txt_dob.Text))
                {
                    var separar = txt_dob.Text.Split('/');
                    if (separar.Length == 3)
                    {
                        try
                        {
                            Convert.ToDateTime(separar[2] + "/" + separar[1] + "/" + separar[0]);
                        }
                        catch
                        {
                            txt_dob.Text = "";
                            txt_dob.Placeholder = "fecha no valida";
                        }
                    }
                }
                #endregion;


                #region guardar foto en la galeria si se cambio por la de defecto;

                if (btn_foto.Tag == 2)
                {

                    var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    var directoryname = System.IO.Path.Combine(documentsDirectory, "FotosUsuario");
                    System.IO.Directory.CreateDirectory(directoryname);
                    string jpgFilename = System.IO.Path.Combine(directoryname, "FotoUsuario.jpg"); // hardcoded filename, overwritten each time. You can make it dynamic as per your requirement.

                    UIImage imagen = new UIImage(jpgFilename);

                    var assetCollection = new PHAssetCollection();
                    var albumFound = false;
                    var assetCollectionPlaceholder = new PHObjectPlaceholder();


                    var fetchOptions = new PHFetchOptions();
                    fetchOptions.Predicate = NSPredicate.FromFormat("title LIKE \"CABASUS\"");
                    var collection = PHAssetCollection.FetchAssetCollections(PHAssetCollectionType.Album, PHAssetCollectionSubtype.Any, fetchOptions);

                    if (collection.firstObject != null)
                    {
                        albumFound = true;
                        assetCollection = collection.firstObject as PHAssetCollection;
                        GuardarFoto(imagen, assetCollection, collection);

                    }
                    else
                    {
                        PHPhotoLibrary.SharedPhotoLibrary.PerformChanges(() =>
                        {

                            var createAlbumRequest = PHAssetCollectionChangeRequest.CreateAssetCollection("CABASUS");
                            assetCollectionPlaceholder = createAlbumRequest.PlaceholderForCreatedAssetCollection;
                        },
                        (ok, error) =>
                        {
                            albumFound = ok;
                            if (ok)
                            {
                                var collectionFetchResult = PHAssetCollection.FetchAssetCollections(new string[] { assetCollectionPlaceholder.LocalIdentifier }, null);
                                Console.WriteLine(collectionFetchResult);
                                collection = collectionFetchResult;
                                assetCollection = collectionFetchResult.firstObject as PHAssetCollection;
                                GuardarFoto(imagen, assetCollection, collection);
                            }
                        });
                    }
                }

                #endregion;

                #region usar API para registrar;

                string server = "http://192.168.1.74:5001/api/Account/registrar";
                string formato = "application/json";

                usuarios us = new usuarios()
                {
                    nombre = txt_username.Text,
                    email = txt_email.Text,
                    contrasena = txt_pw.Text,
                    fecha_nacimiento = txt_dob.Text,
                    id_dispositivo = UIDevice.CurrentDevice.IdentifierForVendor.AsString(),
                    SO = "iOS",
                    tokenFB = new ShareInSide().consultxmlTokenFB().token
                };

                var json = new StringContent(JsonConvert.SerializeObject(us), Encoding.UTF8, formato);
                HttpResponseMessage respuesta = null;
                string content = "";
                try
                {
                    HttpClient cliente = new HttpClient();

                    progreso.StartAnimating();
                    progreso.Hidden = false;

                    respuesta = await cliente.PostAsync(server, json);
                    content = await respuesta.Content.ReadAsStringAsync();

                    respuesta.EnsureSuccessStatusCode();

                    if (respuesta.IsSuccessStatusCode)
                    {
                        Console.WriteLine(content);
                        var contenido = JsonConvert.DeserializeObject<tokens>(content);

                        new ShareInSide().savexmlToken(contenido.token, contenido.expiration);

                        string id = new ShareInSide().conseguirIDUsuarioDelToken(contenido.token);

                        //saber si la foto se cambio o no, para subirla o no XD
                        if (btn_foto.Tag == 2) 
                        {
                            var URL = await new ShareInSide().SubirImagen("usuarios", id);

                            //actualizar la foto en los datos del ususario
                            server = "http://192.168.1.74:5001/api/Usuario/actualizarFoto?URL=" + URL;
                            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new ShareInSide().consultxmlToken().token);
                            respuesta = await cliente.GetAsync(server);
                            content = await respuesta.Content.ReadAsStringAsync();
                            if (respuesta.IsSuccessStatusCode)
                                Console.WriteLine("foto guradada");
                            else
                                Console.WriteLine("no se pudo actualizar la foto");

                            Console.WriteLine(URL);
                        }

                        progreso.StopAnimating();
                        progreso.Hidden = true;

                        var detalle = this.Storyboard.InstantiateViewController("ViewController") as ViewController;
                        detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                        detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                        this.PresentViewController(detalle, true, null);
                    }
                    else
                    {
                        Console.WriteLine(content);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(content);
                }

                #endregion;

                btn_done.Enabled = true;
                btn_foto.Enabled = true;
                progreso.StopAnimating();
                progreso.Hidden = true;
            };

            #endregion;
        }

        public void GuardarFoto(UIImage imagen, PHAssetCollection pHAssetCollection, PHFetchResult pHFetchResult)
        {
            PHPhotoLibrary.SharedPhotoLibrary.PerformChanges(() =>
            {
                var assetRequest = PHAssetChangeRequest.FromImage(imagen);
                var assetPlaceholder = assetRequest.PlaceholderForCreatedAsset;
                var albumChangeRequest = PHAssetCollectionChangeRequest.ChangeRequest(pHAssetCollection, pHFetchResult);
                albumChangeRequest.AddAssets(new PHObject[] { assetPlaceholder });
            }, (ok, error) =>
            {
                Console.WriteLine("added image to album");
                Console.WriteLine(error);
            });
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}

