using System;
using System.Net.Http;
using System.Text;
using CoreGraphics;
using Foundation;
using Photos;
using UIKit;
using CABASUS.Modelos;
using Newtonsoft.Json;

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
            btn_foto.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            btn_foto.Layer.BorderWidth = 1f;
            btn_foto.Layer.CornerRadius = 50f;
            btn_foto.ClipsToBounds = true;

            lbl_username.Frame= new CGRect(25, 120, View.Frame.Width - 50, 30);
            txt_username.Frame = new CGRect(25, 155, View.Frame.Width - 50, 40);
            txt_username.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            txt_username.Layer.BorderWidth = 1f;

            lbl_email.Frame= new CGRect(25,205, View.Frame.Width - 50, 30);
            txt_email.Frame= new CGRect(25, 240, View.Frame.Width - 50, 40);
            txt_email.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            txt_email.Layer.BorderWidth = 1f;

            lbl_pw.Frame = new CGRect(25, 290, View.Frame.Width - 50, 30);
            txt_pw.Frame = new CGRect(25, 325, View.Frame.Width - 50, 40);
            txt_pw.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            txt_pw.Layer.BorderWidth = 1f;

            lbl_dob.Frame = new CGRect(25, 375, View.Frame.Width - 50, 30);
            txt_dob.Frame = new CGRect(25, 410, View.Frame.Width - 50, 40);
            txt_dob.Layer.BorderColor = UIColor.FromRGB(246, 128, 25).CGColor;
            txt_dob.Layer.BorderWidth = 1f;

            btn_done.Frame = new CGRect(25, 550, View.Frame.Width - 50, 40);
            btn_done.Layer.CornerRadius = 20f;
            btn_done.ClipsToBounds = true;

            btn_terms.Frame = new CGRect(25, 595, View.Frame.Width - 50, 60);
            btn_terms.TitleLabel.Lines = 3;
            btn_terms.TitleLabel.TextAlignment=UITextAlignment.Center;

            scroll_register.Frame = new CGRect(0, 55, View.Frame.Width, View.Frame.Height - 55);
            scroll_register.ContentSize= new CGSize(View.Frame.Width, 670);
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

            btn_foto.TouchUpInside+=delegate {

                GaleryCameraAccessController obj = new GaleryCameraAccessController();

                var okCancelAlertController = UIAlertController.Create(null, "Selecciona una opcion", UIAlertControllerStyle.Alert);

                okCancelAlertController.AddAction(UIAlertAction.Create("Galeria", UIAlertActionStyle.Default, alert => obj.BringUpPhotoGallery(btn_foto, 50)));
                okCancelAlertController.AddAction(UIAlertAction.Create("Camara", UIAlertActionStyle.Default, alert => obj.BringUpCamera(btn_foto, 50)));
                okCancelAlertController.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));

                PresentViewController(okCancelAlertController, true, null);
            };

            #endregion;

            #region guardar datos al precionar boton aceptar;

            btn_done.TouchUpInside += async delegate
            {

                #region guardar foto en la galeria;

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
                #endregion;

                #region usar API para registrar;

                string server = "http://192.168.1.73:5001/api/Account/registrar";
                string formato = "application/json";

                usuarios us = new usuarios()
                {
                    nombre = txt_username.Text,
                    email = txt_email.Text,
                    contrasena = txt_pw.Text,
                    fecha_nacimiento = txt_dob.Text
                };

                var json = new StringContent(JsonConvert.SerializeObject(us), Encoding.UTF8, formato);

                try
                {
                    HttpClient cliente = new HttpClient();

                    var respuesta = await cliente.PostAsync(server, json);
                    var content = await respuesta.Content.ReadAsStringAsync();

                    respuesta.EnsureSuccessStatusCode();

                    if (respuesta.IsSuccessStatusCode)
                    {
                        Console.WriteLine(content);
                        var contenido = JsonConvert.DeserializeObject<tokens>(content);

                        new ShareInSide().savexmlToken(contenido.token, contenido.expiration);

                        string id = new ShareInSide().conseguirIDUsuarioDelToken(contenido.token);

                        var detalle = this.Storyboard.InstantiateViewController("ViewController") as ViewController;
                        detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                        detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                        this.PresentViewController(detalle, true, null);
                        //hola
                    }
                    else
                    {
                        Console.WriteLine(content);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                #endregion;
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

