using System;

using Foundation;
using UIKit;

namespace CABASUS.Adapters
{
    public partial class Custom_Cell_Chat_Adapter : UITableViewCell
    {
        public Custom_Cell_Chat_Adapter(IntPtr handle) : base(handle){}

        internal void ActualizarDatos(string mensaje, int origen)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Font = lblMensaje.Font.WithSize(18);

            viewMensaje.Layer.CornerRadius = 10;
            viewMensaje.Layer.MasksToBounds = true;

            if (origen % 2 == 0) 
            {
                constRight.Constant = 100;
                constLeft.Constant = 10;

                viewMensaje.BackgroundColor = UIColor.Blue;
            }
            else{
                constRight.Constant = 10;
                constLeft.Constant = 100;

                viewMensaje.BackgroundColor = UIColor.Yellow;
            }
        }
    }
}