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
    [Register ("Sesion_ViewController")]
    partial class Sesion_ViewController
    {
        [Outlet]
        UIKit.UIButton btn_login { get; set; }


        [Outlet]
        UIKit.UIButton btn_recovery { get; set; }


        [Outlet]
        UIKit.UIButton btnback { get; set; }


        [Outlet]
        UIKit.UILabel lbl_login { get; set; }


        [Outlet]
        UIKit.UILabel lbl_password { get; set; }


        [Outlet]
        UIKit.UILabel lblemail { get; set; }


        [Outlet]
        UIKit.UIActivityIndicatorView progreso { get; set; }


        [Outlet]
        UIKit.UITextField txt_email { get; set; }


        [Outlet]
        UIKit.UITextField txt_password { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btn_login != null) {
                btn_login.Dispose ();
                btn_login = null;
            }

            if (btn_recovery != null) {
                btn_recovery.Dispose ();
                btn_recovery = null;
            }

            if (btnback != null) {
                btnback.Dispose ();
                btnback = null;
            }

            if (lbl_login != null) {
                lbl_login.Dispose ();
                lbl_login = null;
            }

            if (lbl_password != null) {
                lbl_password.Dispose ();
                lbl_password = null;
            }

            if (lblemail != null) {
                lblemail.Dispose ();
                lblemail = null;
            }

            if (progreso != null) {
                progreso.Dispose ();
                progreso = null;
            }

            if (txt_email != null) {
                txt_email.Dispose ();
                txt_email = null;
            }

            if (txt_password != null) {
                txt_password.Dispose ();
                txt_password = null;
            }
        }
    }
}