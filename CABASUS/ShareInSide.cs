using System;
using System.IO;
using System.Xml.Serialization;
using CABASUS.Modelos;

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
    }
}