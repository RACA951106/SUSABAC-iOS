using System;
using CoreGraphics;
using Foundation;
using UIKit;
using System.Net.Http;
using CABASUS.Modelos;
using System.Net;
using System.IO;

namespace CABASUS.Adapters
{
    public class Collection_Adapter_Celda : UICollectionViewCell
    {
        public UIImageView img;
        public UILabel lblNombre;
        public UIActivityIndicatorView progreso;

        [Export("initWithFrame:")]
        public Collection_Adapter_Celda(CGRect frame) : base(frame)
        {
            img = new UIImageView();
            lblNombre = new UILabel();
            progreso = new UIActivityIndicatorView();

            lblNombre.Text = "Fotitos";
            lblNombre.Frame = new CGRect(0, frame.Width - 30, frame.Width, 30);
            lblNombre.TextAlignment = UITextAlignment.Center;
            lblNombre.TextColor = UIColor.White;
            lblNombre.BackgroundColor = UIColor.FromRGBA(0, 0, 0, .5f);

            img.Frame = new CGRect(0, 0, frame.Width, frame.Width);


            progreso.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
            progreso.Frame = new CGRect((frame.Width / 2) - 10, (frame.Height / 2) - 10, 20, 20);
            progreso.Hidden = true;

            ContentView.Layer.BorderWidth = 1;
            ContentView.Layer.BorderColor = UIColor.White.CGColor;
            ContentView.AddSubviews(img, lblNombre, progreso);
        }

        public async void updateCell(caballos caballo)
        {
            //
            try
            {
                var r = new Random();
                lblNombre.Text = caballo.nombre;

                var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var directoryname = Path.Combine(documentsDirectory, "Temporal");
                Directory.CreateDirectory(directoryname);
                string jpgFilename = Path.Combine(directoryname, caballo.id_caballo + ".jpg"); // hardcoded filename, overwritten each time. You can make it dynamic as per your requirement.


                if (File.Exists(jpgFilename))
                {
                    img.Image = UIImage.FromFile(jpgFilename);
                }
                else
                {
                    progreso.Hidden = false;
                    progreso.StartAnimating();
                    await new ShareInSide().descargaTemporal(caballo.foto, caballo.id_caballo);
                    img.Image = UIImage.FromFile(jpgFilename);
                    progreso.StopAnimating();
                    progreso.Hidden = true;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
