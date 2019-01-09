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
    [Register ("Register_ViewController")]
    partial class Register_ViewController
    {
        [Outlet]
        UIKit.UIButton btn_done { get; set; }


        [Outlet]
        UIKit.UIButton btn_foto { get; set; }


        [Outlet]
        UIKit.UIButton btn_terms { get; set; }


        [Outlet]
        UIKit.UIButton btnback { get; set; }


        [Outlet]
        UIKit.UILabel lbl_dob { get; set; }


        [Outlet]
        UIKit.UILabel lbl_email { get; set; }


        [Outlet]
        UIKit.UILabel lbl_pw { get; set; }


        [Outlet]
        UIKit.UILabel lbl_username { get; set; }


        [Outlet]
        UIKit.UIActivityIndicatorView progreso { get; set; }


        [Outlet]
        UIKit.UIScrollView scroll_register { get; set; }


        [Outlet]
        UIKit.UITextField txt_dob { get; set; }


        [Outlet]
        UIKit.UITextField txt_email { get; set; }


        [Outlet]
        UIKit.UITextField txt_pw { get; set; }


        [Outlet]
        UIKit.UITextField txt_username { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btn_done != null) {
                btn_done.Dispose ();
                btn_done = null;
            }

            if (btn_foto != null) {
                btn_foto.Dispose ();
                btn_foto = null;
            }

            if (btn_terms != null) {
                btn_terms.Dispose ();
                btn_terms = null;
            }

            if (btnback != null) {
                btnback.Dispose ();
                btnback = null;
            }

            if (lbl_dob != null) {
                lbl_dob.Dispose ();
                lbl_dob = null;
            }

            if (lbl_email != null) {
                lbl_email.Dispose ();
                lbl_email = null;
            }

            if (lbl_pw != null) {
                lbl_pw.Dispose ();
                lbl_pw = null;
            }

            if (lbl_username != null) {
                lbl_username.Dispose ();
                lbl_username = null;
            }

            if (progreso != null) {
                progreso.Dispose ();
                progreso = null;
            }

            if (scroll_register != null) {
                scroll_register.Dispose ();
                scroll_register = null;
            }

            if (txt_dob != null) {
                txt_dob.Dispose ();
                txt_dob = null;
            }

            if (txt_email != null) {
                txt_email.Dispose ();
                txt_email = null;
            }

            if (txt_pw != null) {
                txt_pw.Dispose ();
                txt_pw = null;
            }

            if (txt_username != null) {
                txt_username.Dispose ();
                txt_username = null;
            }
        }
    }
}