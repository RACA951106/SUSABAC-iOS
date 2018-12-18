using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CABASUS.Modelos;
using CoreGraphics;
using Foundation;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=susabac;AccountKey=p6EvYU5CRlr7U3nXEp0A+Q/M1ZRtReQjomO8EwaBJ00LxKoo/7MG/m7aX7pbdJGGcJ0HcYGzn6LM7lFYbMeR+g==;EndpointSuffix=core.windows.net");
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(Contenedor);

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(Nombre + ".jpg");
            cloudBlockBlob.Properties.ContentType = "image/jpg";

            try
            {
                var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var directoryname = System.IO.Path.Combine(documentsDirectory, "FotosUsuario");
                System.IO.Directory.CreateDirectory(directoryname);
                string jpgFilename = System.IO.Path.Combine(directoryname, "FotoUsuario.jpg");

                await cloudBlockBlob.UploadFromFileAsync(jpgFilename);
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
    }
}