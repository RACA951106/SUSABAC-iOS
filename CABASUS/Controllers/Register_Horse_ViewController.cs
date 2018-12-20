using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CABASUS.Modelos;
using CoreGraphics;
using Foundation;
using Newtonsoft.Json;
using Photos;
using SQLite;
using UIKit;

namespace CABASUS.Controllers
{
    public partial class Register_Horse_ViewController : UIViewController
    {
        string ip = "192.168.1.74";
        ShareInSide S = new ShareInSide();
        UIButton alerta;
        UITableView listViewBreed, listViewGender;
        string formato = "application/json";
        public bool indicadorAccion = true;

        public Register_Horse_ViewController() : base("Register_Horse_ViewController", null)
        {
        }

        protected Register_Horse_ViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var prueba = indicadorAccion;
            var sa = "";
            // Perform any additional setup after loading the view, typically from a nib.

            #region Medidas y ubicacion de objetos en la interfaz 

            btn_back.Frame = new CGRect(0, 35, 50, 20);

            btn_foto.Frame = new CGRect((View.Frame.Width / 2) - 50, 0, 100, 100);
            btn_foto.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            btn_foto.Layer.BorderWidth = 1f;
            btn_foto.Layer.CornerRadius = 50f;
            btn_foto.ClipsToBounds = true;
            btn_foto.Tag = 1;

            lbl_NameHorse.Frame = new CGRect(25, 120, View.Frame.Width - 50, 30);
            txt_nameHorse.Frame = new CGRect(25, 155, View.Frame.Width - 50, 40);
            txt_nameHorse.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_nameHorse.Layer.BorderWidth = 1f;

            lbl_weight.Frame = new CGRect(25, 205, View.Frame.Width - 50, 30);
            txt_weight.Frame = new CGRect(25, 240, View.Frame.Width - 50, 40);
            txt_weight.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_weight.Layer.BorderWidth = 1f;

            lbl_height.Frame = new CGRect(25, 290, View.Frame.Width - 50, 30);
            txt_height.Frame = new CGRect(25, 325, View.Frame.Width - 50, 40);
            txt_height.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_height.Layer.BorderWidth = 1f;

            lbl_breed.Frame = new CGRect(25, 375, View.Frame.Width - 50, 30);
            txt_breed.Frame = new CGRect(25, 410, View.Frame.Width - 50, 40);
            txt_breed.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_breed.Layer.BorderWidth = 1f;

            lbl_dob.Frame = new CGRect(25, 460, View.Frame.Width - 50, 30);
            txt_dob.Frame = new CGRect(25, 495, View.Frame.Width - 50, 40);
            txt_dob.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_dob.Layer.BorderWidth = 1f;

            lbl_gender.Frame = new CGRect(25, 545, View.Frame.Width - 50, 30);
            txt_gender.Frame = new CGRect(25, 580, View.Frame.Width - 50, 40);
            txt_gender.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_gender.Layer.BorderWidth = 1f;

            lbl_oat.Frame = new CGRect(25, 630, View.Frame.Width - 50, 30);
            txt_oat.Frame = new CGRect(25, 665, View.Frame.Width - 50, 40);
            txt_oat.Layer.BorderColor = UIColor.FromRGB(203, 30, 30).CGColor;
            txt_oat.Layer.BorderWidth = 1f;

            btn_done.Frame = new CGRect(25, 770, View.Frame.Width - 50, 40);
            btn_done.Layer.CornerRadius = 20f;
            btn_done.ClipsToBounds = true;


            scroll.Frame = new CGRect(0, 55, View.Frame.Width, View.Frame.Height - 55);
            scroll.ContentSize = new CGSize(View.Frame.Width, 850);

            progress.Frame = new CGRect((View.Frame.Width / 2) - (progress.Frame.Width / 2), (View.Frame.Height / 2) - (progress.Frame.Height / 2), progress.Frame.Width, progress.Frame.Height);
            progress.Hidden = true;
            #endregion;

            #region Que no se copie nada de texto en cajas de double
            txt_weight.EditingChanged += delegate
            {
                try
                {
                    double.Parse(txt_weight.Text);
                    txt_weight.BackgroundColor = UIColor.White;
                }
                catch (Exception ex)
                {
                    txt_weight.Text = "";
                }
            };

            txt_height.EditingChanged += delegate
            {
                try
                {
                    double.Parse(txt_height.Text);
                    txt_height.BackgroundColor = UIColor.White;
                }
                catch (Exception ex)
                {
                    txt_height.Text = "";
                }
            };
            txt_oat.EditingChanged += delegate
            {
                try
                {
                    double.Parse(txt_oat.Text);
                    txt_oat.BackgroundColor = UIColor.White;
                }
                catch (Exception ex)
                {
                    txt_oat.Text = "";
                }
            };




            #endregion

            #region ocultar teclado al tocar la pantalla;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);

            #endregion;

            #region mover pantalla cuando se este llenando el formulario;

            UIKeyboard.Notifications.ObserveWillShow(async (sender, args) =>
            {
                Action action = () =>
                {
                    scroll.ContentSize = new CGSize(View.Frame.Width, 850 + args.FrameEnd.Height);
                };

                UIViewPropertyAnimator animator = new UIViewPropertyAnimator(.3, UIViewAnimationCurve.Linear, action);
                animator.StartAnimation();

                if (txt_nameHorse.IsEditing)
                    accionTeclado(txt_nameHorse, args.FrameEnd);
                if (txt_dob.IsEditing)
                    accionTeclado(txt_dob, args.FrameEnd);
                if (txt_oat.IsEditing)
                    accionTeclado(txt_oat, args.FrameEnd);
                if (txt_height.IsEditing)
                    accionTeclado(txt_height, args.FrameEnd);
                if (txt_weight.IsEditing)
                    accionTeclado(txt_weight, args.FrameEnd);
            });


            UIKeyboard.Notifications.ObserveWillHide((sender, args) =>
            {
                Action action = () =>
                {
                    scroll.ContentSize = new CGSize(View.Frame.Width, 850);
                };

                UIViewPropertyAnimator animator = new UIViewPropertyAnimator(.3, UIViewAnimationCurve.Linear, action);
                animator.StartAnimation();
            });

            #endregion;

            #region abrir camara o galeria para la foto del ususario;

            btn_foto.TouchUpInside += delegate
            {
                GaleryCameraAccessController obj = new GaleryCameraAccessController();

                var okCancelAlertController = UIAlertController.Create(null, "Selecciona una opcion", UIAlertControllerStyle.Alert);

                okCancelAlertController.AddAction(UIAlertAction.Create("Galeria", UIAlertActionStyle.Default, alert => obj.BringUpPhotoGallery(btn_foto, 50)));
                okCancelAlertController.AddAction(UIAlertAction.Create("Camara", UIAlertActionStyle.Default, alert => obj.BringUpCamera(btn_foto, 50)));
                okCancelAlertController.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));

                PresentViewController(okCancelAlertController, true, null);
            };

            #endregion;

            #region comprobar que no se pegue nada en la fecha

            txt_dob.EditingChanged += delegate
            {

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

            txt_gender.TouchUpInside += delegate
            {

                var contenedor = new UIView();
                if (View.Frame.Width == 320)
                    contenedor.Frame = new CGRect(30, 120, View.Frame.Width - 60, View.Frame.Height - 240);
                else if (View.Frame.Width == 414)
                    contenedor.Frame = new CGRect(50, 250, View.Frame.Width - 100, View.Frame.Height - 500);
                else
                    contenedor.Frame = new CGRect(50, 150, View.Frame.Width - 100, View.Frame.Height - 300);

                var lblGender = new UILabel(new CGRect(0, 10, contenedor.Frame.Width, 20));
                lblGender.Text = "Gender";
                lblGender.TextAlignment = UITextAlignment.Center;
                lblGender.Font = UIFont.FromName("Arial", 17);

                listViewGender = new UITableView(new CGRect(5, 40, contenedor.Frame.Width - 10, contenedor.Frame.Height - 40));

                contenedor.AddSubviews(lblGender, listViewGender);

                GenerarAlerta(View, contenedor, 2);

            };

            txt_breed.TouchUpInside += delegate
            {

                #region Elementos graficos vista breed

                var contenedor = new UIView();
                if (View.Frame.Width == 320)
                    contenedor.Frame = new CGRect(30, 120, View.Frame.Width - 60, View.Frame.Height - 240);
                else if (View.Frame.Width == 414)
                    contenedor.Frame = new CGRect(50, 250, View.Frame.Width - 100, View.Frame.Height - 500);
                else
                    contenedor.Frame = new CGRect(50, 150, View.Frame.Width - 100, View.Frame.Height - 300);

                var lblBreed = new UILabel(new CGRect(0, 10, contenedor.Frame.Width, 20));
                lblBreed.Text = "Breed";
                lblBreed.TextAlignment = UITextAlignment.Center;
                lblBreed.Font = UIFont.FromName("Arial", 17);

                var txtSearch = new UITextField(new CGRect(5, 35, contenedor.Frame.Width - 40, 30));
                txtSearch.Placeholder = "Search";

                var imgIconSearch = new UIImageView(new CGRect(txtSearch.Frame.Width + 7.5, 37.5, 25, 25));
                imgIconSearch.Image = UIImage.FromFile("lupa_icon").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                imgIconSearch.TintColor = UIColor.FromRGB(215, 51, 39);

                listViewBreed = new UITableView(new CGRect(5, 70, contenedor.Frame.Width - 10, contenedor.Frame.Height - 75));

                contenedor.AddSubviews(lblBreed, txtSearch, imgIconSearch, listViewBreed);

                GenerarAlerta(View, contenedor, 1);

                txtSearch.EditingChanged += delegate
                {
                    var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "RazasGender.sqlite");
                    var con = new SQLiteConnection(ruta);
                    var consulta = con.Query<razas>("SELECT * FROM Razas WHERE raza like '%" + txtSearch.Text + "%'", new razas().id_raza);

                    listViewBreed.Source = new Adapters.Breed_Adapter(consulta, txt_breed, alerta);
                    //listView.ScrollToRow(NSIndexPath.FromRowSection(0,0), UITableViewScrollPosition.None, true);
                    listViewBreed.ReloadData();
                };


                #endregion

            };

            #region guardar datos al precionar boton aceptar;

            btn_done.TouchUpInside += async delegate
            {
                btn_done.Enabled = false;
                btn_foto.Enabled = false;


                #region comproba campos del formulario;
                //  No obligatorio genero
                bool horse = false, weight = false, height = false,
                breed = false, oat = false;

                if (string.IsNullOrEmpty(txt_nameHorse.Text))
                {
                    txt_nameHorse.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                    txt_nameHorse.Placeholder = "llene este campo";
                    horse = false;
                }
                else
                    horse = true;

                if (string.IsNullOrEmpty(txt_weight.Text))
                {
                    txt_weight.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                    txt_weight.Placeholder = "llene este campo";
                    weight = false;
                }
                else
                    weight = true;

                if (string.IsNullOrEmpty(txt_height.Text))
                {
                    txt_height.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                    txt_height.Placeholder = "llene este campo";
                    height = false;
                }
                else
                    height = true;

                if (txt_breed.Title(UIControlState.Normal).Equals("Choose one"))
                {
                    txt_breed.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                    txt_breed.SetTitle("llene este campo", UIControlState.Normal);
                    txt_breed.SetTitleColor(UIColor.Gray, UIControlState.Normal);
                    breed = false;
                }
                else
                    breed = true;

                if (string.IsNullOrEmpty(txt_oat.Text))
                {
                    txt_oat.BackgroundColor = UIColor.FromRGB(255, 178, 178);
                    txt_oat.Placeholder = "llene este campo";
                    oat = false;
                }
                else
                    oat = true;


                //cambiar cuando vuelva a editar
                txt_nameHorse.EditingDidBegin += delegate
                {

                    txt_nameHorse.BackgroundColor = UIColor.White;
                };
                txt_weight.EditingDidBegin += delegate
                {
                    txt_weight.BackgroundColor = UIColor.White;
                };

                txt_height.EditingDidBegin += delegate
                {
                    txt_height.BackgroundColor = UIColor.White;
                };

                txt_breed.EditingDidEnd += delegate
                {
                    txt_breed.BackgroundColor = UIColor.White;
                };

                txt_oat.EditingDidEnd += delegate
                {
                    txt_oat.BackgroundColor = UIColor.White;
                };

                txt_breed.TouchUpInside += delegate
                {
                    txt_breed.SetTitleColor(UIColor.FromRGB(203, 203, 208), UIControlState.Normal);
                    txt_breed.SetTitle("Choose one", UIControlState.Normal);
                    txt_breed.BackgroundColor = UIColor.White;
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

                if (horse && weight && height && breed && oat)
                {
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
                            S.GuardarFoto(imagen, assetCollection, collection);

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
                                    S.GuardarFoto(imagen, assetCollection, collection);
                                }
                            });
                        }
                    }

                    #endregion;

                    #region APIS caballo
                    string serverActualizar = "http://" + ip + ":5001/api/Caballo/actualizar";
                    string server = "http://" + ip + ":5001/api/Caballo/registrar";

                    if (indicadorAccion)
                    {
                        #region usar API para registrar;

                        caballos us = new caballos()
                        {
                            nombre = txt_nameHorse.Text,
                            peso = double.Parse(txt_weight.Text),
                            altura = double.Parse(txt_height.Text),
                            raza = 1,
                            fecha_nacimiento = txt_dob.Text,
                            genero = 1,
                            foto = "",
                            avena = double.Parse(txt_oat.Text)
                        };

                        var json = new StringContent(JsonConvert.SerializeObject(us), Encoding.UTF8, formato);
                        HttpResponseMessage respuesta = null;
                        string content = "";

                        try
                        {
                            HttpClient cliente = new HttpClient();
                            cliente.Timeout = TimeSpan.FromSeconds(20);

                            progress.StartAnimating();
                            progress.Hidden = false;

                            txt_nameHorse.Enabled = false;
                            txt_weight.Enabled = false;
                            txt_height.Enabled = false;
                            txt_breed.Enabled = false;
                            txt_dob.Enabled = false;
                            txt_gender.Enabled = false;
                            txt_oat.Enabled = false;

                            btn_done.Enabled = false;
                            btn_foto.Enabled = false;

                            cliente.DefaultRequestHeaders.Authorization =
                                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new ShareInSide().consultxmlToken().token);

                            respuesta = await cliente.PostAsync(server, json);

                            content = await respuesta.Content.ReadAsStringAsync();

                            respuesta.EnsureSuccessStatusCode();

                            if (respuesta.IsSuccessStatusCode)
                            {
                                Console.WriteLine(content);


                                //saber si la foto se cambio o no, para subirla o no XD
                                if (btn_foto.Tag == 2)
                                {
                                    var URL = await new ShareInSide().SubirImagen("caballos", content);

                                    //actualizar la foto en los datos del ususario
                                    server = "http://" + ip + ":5001/api/Caballo/actualizarFoto";


                                    var claseSubirFoto = new caballos
                                    {
                                        foto = URL,
                                        id_caballo = content
                                    };

                                    respuesta = await cliente.PostAsync(server, new StringContent(JsonConvert.SerializeObject(claseSubirFoto), Encoding.UTF8, formato));

                                    content = await respuesta.Content.ReadAsStringAsync();
                                    if (respuesta.IsSuccessStatusCode)
                                        Console.WriteLine("foto guradada");
                                    else
                                        Console.WriteLine("no se pudo actualizar la foto");

                                    Console.WriteLine(URL);
                                }

                                progress.StopAnimating();
                                progress.Hidden = true;

                                var detalle = this.Storyboard.InstantiateViewController("Tabs_ViewController") as Tabs_ViewController;
                                detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                                detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                                this.PresentViewController(detalle, true, null);
                            }
                            else
                            {
                                Console.WriteLine(content);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(content);
                        }

                        #endregion;
                    }
                    else
                    {
                        #region usar API para actualizar
                        #endregion
                    }


                    #endregion
                }

                btn_done.Enabled = true;
                btn_foto.Enabled = true;

                progress.StopAnimating();
                progress.Hidden = true;

                txt_nameHorse.Enabled = true;
                txt_weight.Enabled = true;
                txt_height.Enabled = true;
                txt_breed.Enabled = true;
                txt_dob.Enabled = true;
                txt_gender.Enabled = true;
                txt_oat.Enabled = true;
            };

            #endregion;


        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void GenerarAlerta(UIView view, UIView contenido, int source)
        {
            #region Consulta base interna
            var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "RazasGender.sqlite");
            var con = new SQLiteConnection(ruta);
            var consulta = con.Query<razas>("SELECT * FROM Razas", new razas().id_raza);
            #endregion;

            alerta = new UIButton(new CGRect(View.Frame.Width, View.Frame.Height, 0, 0));
            alerta.BackgroundColor = new UIColor(0 / 255, 0 / 255, 0 / 255, 127.5f / 255);

            UIView alerta1 = new UIView(new CGRect(View.Frame.Width, View.Frame.Height, 0, 0));
            alerta1.BackgroundColor = UIColor.White;

            alerta.AddSubview(alerta1);
            view.AddSubview(alerta);

            if (source == 1)
                listViewBreed.Source = new Adapters.Breed_Adapter(consulta, txt_breed, alerta);
            else
                listViewGender.Source = new Adapters.Gender_Adapter(txt_gender, alerta);

            Action a = () =>
            {
                alerta.Frame = new CGRect(0, 0, View.Frame.Width, View.Frame.Height);
                alerta1.Frame = contenido.Frame;

            };
            UIViewPropertyAnimator Animar = new UIViewPropertyAnimator(.3, UIViewAnimationCurve.EaseInOut, a);
            Animar.StartAnimation();

            contenido.Frame = new CGRect(0, 0, alerta1.Frame.Width, alerta1.Frame.Height);
            alerta1.AddSubview(contenido);
            alerta.TouchUpInside += delegate
            {

                progress.Frame = new CGRect((View.Frame.Width / 2) - (progress.Frame.Width / 2), (View.Frame.Height / 1.5) - (progress.Frame.Height / 2), progress.Frame.Width, progress.Frame.Height);

                alerta.RemoveFromSuperview();
            };
        }

        private void accionTeclado(UITextField cajaTexto, CGRect teclado)
        {
            var vistaNivelScroll = View.Frame.Height - 35;
            var visible = vistaNivelScroll - teclado.Height;

            var frameCaja = View.ConvertRectFromView(cajaTexto.Frame, scroll);
            var ycajatexto = frameCaja.Y - teclado.Y;
            ycajatexto += (cajaTexto.Frame.Height + 50);

            if(frameCaja.Y + cajaTexto.Frame.Height > teclado.Y)
                scroll.SetContentOffset(new CGPoint(0, scroll.ContentOffset.Y + ycajatexto), true);

        }
    }
}