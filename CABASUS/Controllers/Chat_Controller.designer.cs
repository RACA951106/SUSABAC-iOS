// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CABASUS.Controllers
{
    [Register ("Chat_Controller")]
    partial class Chat_Controller
    {
        [Outlet]
        UIKit.UIButton btnBack { get; set; }


        [Outlet]
        UIKit.UIButton btnEnviarMensaje { get; set; }


        [Outlet]
        UIKit.UILabel lblNombre { get; set; }


        [Outlet]
        UIKit.UITableView tableChat { get; set; }


        [Outlet]
        UIKit.UITextField txtMensaje { get; set; }


        [Outlet]
        UIKit.UIView viewFondo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnBack != null) {
                btnBack.Dispose ();
                btnBack = null;
            }

            if (btnEnviarMensaje != null) {
                btnEnviarMensaje.Dispose ();
                btnEnviarMensaje = null;
            }

            if (lblNombre != null) {
                lblNombre.Dispose ();
                lblNombre = null;
            }

            if (tableChat != null) {
                tableChat.Dispose ();
                tableChat = null;
            }

            if (txtMensaje != null) {
                txtMensaje.Dispose ();
                txtMensaje = null;
            }

            if (viewFondo != null) {
                viewFondo.Dispose ();
                viewFondo = null;
            }
        }
    }
}