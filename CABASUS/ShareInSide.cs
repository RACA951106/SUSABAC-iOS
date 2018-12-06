using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CABASUS.Modelos;
using Foundation;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

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

    }
}