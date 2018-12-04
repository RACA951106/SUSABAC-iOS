using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CABASUS.Modelos;
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
    }
}