﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CABASUS.Controllers;
using CABASUS.Modelos;
using CoreGraphics;
using Foundation;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ObjCRuntime;
using Photos;
using Security;
using UIKit;

namespace CABASUS
{
    public class ShareInSide
    {
        public void savexmlToken(string token, string expiration)
        {
            var tokens = new tokens();
            tokens.token = token;
            tokens.expiration = expiration;

            var data = new XmlSerializer(typeof(tokens));
            var Escritura = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "token.xml"));
            data.Serialize(Escritura, tokens);
            Escritura.Close();
        }

        public tokens consultxmlToken()
        {
            var serializador = new XmlSerializer(typeof(tokens));
            var Lectura = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "token.xml"));
            var datos = (tokens)serializador.Deserialize(Lectura);
            Lectura.Close();
            return datos;
        }

        public void savexmlUsuario(usuarios usuario)
        {

            var data = new XmlSerializer(typeof(usuarios));
            var Escritura = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "usuario.xml"));
            data.Serialize(Escritura, usuario);
            Escritura.Close();
        }

        public usuarios consultxmlUsuario()
        {
            var serializador = new XmlSerializer(typeof(usuarios));
            var Lectura = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "usuario.xml"));
            var datos = (usuarios)serializador.Deserialize(Lectura);
            Lectura.Close();
            return datos;
        }

        public void savexmlTokenFB(string token)
        {
            var tokens = new tokens();
            tokens.token = token;

            var data = new XmlSerializer(typeof(tokens));
            var Escritura = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "tokenFB.xml"));
            data.Serialize(Escritura, tokens);
            Escritura.Close();
        }

        public tokens consultxmlTokenFB()
        {
            var serializador = new XmlSerializer(typeof(tokens));
            var Lectura = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "tokenFB.xml"));
            var datos = (tokens)serializador.Deserialize(Lectura);
            Lectura.Close();
            return datos;
        }

        public void saveTabState(string estado)
        {
            var tokens = new tokens();
            tokens.token = estado;

            var data = new XmlSerializer(typeof(tokens));
            var Escritura = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "TabState.xml"));
            data.Serialize(Escritura, tokens);
            Escritura.Close();
        }

        public string consulTabState()
        {
            var serializador = new XmlSerializer(typeof(tokens));
            var Lectura = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "TabState.xml"));
            var datos = (tokens)serializador.Deserialize(Lectura);
            Lectura.Close();
            return datos.token;
        }

        public string conseguirIDUsuarioDelToken(string token)
        {
            var handler = new JwtSecurityTokenHandler(); 

            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "id").Value;
            return jti;
        }

        public async Task<string> SubirImagen(string Contenedor, string Nombre)
        {
            try
            { 
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=susabac;AccountKey=p6EvYU5CRlr7U3nXEp0A+Q/M1ZRtReQjomO8EwaBJ00LxKoo/7MG/m7aX7pbdJGGcJ0HcYGzn6LM7lFYbMeR+g==;EndpointSuffix=core.windows.net");
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(Contenedor);

                if (await cloudBlobContainer.CreateIfNotExistsAsync(new BlobRequestOptions() { ServerTimeout = TimeSpan.FromSeconds(30) }, null))
                {
                    await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    });
                }

                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(Nombre + ".jpg");
                cloudBlockBlob.Properties.ContentType = "image/jpg";

                var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var directoryname = System.IO.Path.Combine(documentsDirectory, "FotosUsuario");
                System.IO.Directory.CreateDirectory(directoryname);
                string jpgFilename = System.IO.Path.Combine(directoryname, "FotoUsuario.jpg");

                await cloudBlockBlob.UploadFromFileAsync(jpgFilename, null, new BlobRequestOptions() { ServerTimeout = TimeSpan.FromSeconds(20), MaximumExecutionTime = TimeSpan.FromSeconds(20) }, null);
                return cloudBlockBlob.Uri.AbsoluteUri;
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        const int _downloadImageTimeoutInSeconds = 15;
        readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(_downloadImageTimeoutInSeconds) };

        public async Task<bool> DownloadImageAsync(string imageUrl)
        {
            try
            {
                using (var httpResponse = await _httpClient.GetAsync(imageUrl))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var img = await httpResponse.Content.ReadAsByteArrayAsync();

                        var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        var directoryname = Path.Combine(documentsDirectory, "FotosUsuario");
                        Directory.CreateDirectory(directoryname);
                        string jpgFilename = Path.Combine(directoryname, "FotosUsuario.jpg"); // hardcoded filename, overwritten each time. You can make it dynamic as per your requirement.

                        NSData data = NSData.FromArray(img);
                        NSError error = null;
                        data.Save(jpgFilename, false, out error);
                        return true;

                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> descargaTemporal(string ulr, string nombreArchivo)
        {
            try
            {
                using (var httpResponse = await _httpClient.GetAsync(ulr))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var img = await httpResponse.Content.ReadAsByteArrayAsync();
                        var size = new CGSize(200, 200);

                        UIImage image = new UIImage();
                        NSData data = NSData.FromArray(img);
                        image = UIImage.LoadFromData(data);

                        nfloat originalWidth = image.Size.Width;
                        nfloat originalHeight = image.Size.Height;
                        nfloat originalRatio = originalWidth / originalHeight;

                        nfloat targetRatio = size.Width / size.Height;

                        var targetFrame = new CGRect(x: 0.0, y: 0.0, width: size.Width, height: size.Height);

                        if (originalRatio > targetRatio)
                        {

                            nfloat targetHeight = size.Height;
                            nfloat targetWidth = targetHeight * originalRatio;
                            targetFrame = new CGRect(x: (size.Width - targetWidth) * 0.5, y: (size.Height - targetHeight) * 0.5, width: targetWidth, height: targetHeight);

                        }
                        else if (originalRatio < targetRatio)
                        {
                            nfloat targetWidth = size.Width;
                            nfloat targetHeight = targetWidth / originalRatio;
                            targetFrame = new CGRect(x: (size.Width - targetWidth) * 0.5, y: (size.Height - targetHeight) * 0.5, width: targetWidth, height: targetHeight);
                        }

                        UIGraphics.BeginImageContext(size);
                        image.Draw(targetFrame);
                        var imageToSave = UIGraphics.GetImageFromCurrentImageContext();
                        UIGraphics.EndImageContext();

                        var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        var directoryname = Path.Combine(documentsDirectory, "Temporal");
                        Directory.CreateDirectory(directoryname);
                        string jpgFilename = Path.Combine(directoryname, nombreArchivo + ".jpg"); // hardcoded filename, overwritten each time. You can make it dynamic as per your requirement.

                        NSError error = null;
                        imageToSave.AsJPEG().Save(jpgFilename, false, out error);
                        return true;

                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                return false;
            }
        }

        public void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }


        public async Task<bool> EliminarImagen(string Contenedor, string id)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(
               "DefaultEndpointsProtocol=https;" +
               "AccountName=susabac;" +
               "AccountKey=p6EvYU5CRlr7U3nXEp0A+Q/M1ZRtReQjomO8EwaBJ00LxKoo/7MG/m7aX7pbdJGGcJ0HcYGzn6LM7lFYbMeR+g==;EndpointSuffix=core.windows.net");

            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(Contenedor);
            CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(id + ".jpg");

            if (await blockBlob.DeleteIfExistsAsync())
            {
                var RutaImage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), id + ".jpg");
                if (!File.Exists(RutaImage))
                    File.Delete(RutaImage);

                return true;
            }  
            else
                return false;
        }

        public void Toast(string mensaje)
        {
            UIAlertView alert = new UIAlertView()
            {
                Message = mensaje
            };
            alert.AddButton("OK");
            alert.Show();
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

        public string GetLocalFilePath(string filename, string nombreenresources, string tipoenresources)
        {
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbPath = Path.Combine(docFolder, filename);
            CopyDatabaseIfNotExists(dbPath, nombreenresources, tipoenresources);

            return dbPath;
        }

        private void CopyDatabaseIfNotExists(string dbPath, string nameinres, string typeinres)
        {
            if (!File.Exists(dbPath))
            {
                var existingDb = NSBundle.MainBundle.PathForResource(nameinres, typeinres);
                File.Copy(existingDb, dbPath);
            }
        }

        public void Toastquestion(string mensaje, string pantalla, UIViewController controlador)
        {
            UIAlertView alert = new UIAlertView()
            {
                Message = mensaje
            };
            alert.AddButton("Si");
            alert.AddButton("No");
            alert.Delegate = new alert_delegate(controlador,pantalla);
            alert.Show();
        }
    }

    public class alert_delegate : UIAlertViewDelegate
    {
        UIViewController controlador;
        string _pantalla;
        public alert_delegate(UIViewController controller,string pantalla)
        {
            controlador = controller;
            _pantalla = pantalla;
        }

        [Export("alertView:clickedButtonAtIndex:")]
        public override void Clicked(UIAlertView alertview, nint buttonIndex)
        {
            switch (_pantalla)
            {
                case "settings":
                    if (buttonIndex == 0)
                    {
                        var detalle = controlador.Storyboard.InstantiateViewController("Register_ViewController") as Register_ViewController;
                        detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                        detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                        controlador.PresentViewController(detalle, true, null);
                    }
                    break;
            }
        }
    }
}
