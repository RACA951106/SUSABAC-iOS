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
    [Register ("Notifications_ViewController")]
    partial class Notifications_ViewController
    {
        [Outlet]
        UIKit.UIImageView imgLupa { get; set; }


        [Outlet]
        UIKit.UITableView tableChat { get; set; }


        [Outlet]
        UIKit.UITextField txtBuscador { get; set; }


        [Outlet]
        UIKit.UIView viewFondoBuscador { get; set; }


        [Outlet]
        UIKit.UIView viewLineaBuscador { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgLupa != null) {
                imgLupa.Dispose ();
                imgLupa = null;
            }

            if (tableChat != null) {
                tableChat.Dispose ();
                tableChat = null;
            }

            if (txtBuscador != null) {
                txtBuscador.Dispose ();
                txtBuscador = null;
            }

            if (viewFondoBuscador != null) {
                viewFondoBuscador.Dispose ();
                viewFondoBuscador = null;
            }

            if (viewLineaBuscador != null) {
                viewLineaBuscador.Dispose ();
                viewLineaBuscador = null;
            }
        }
    }
}