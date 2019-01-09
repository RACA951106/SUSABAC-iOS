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
    [Register ("Login_ViewController")]
    partial class Login_ViewController
    {
        [Outlet]
        UIKit.UIButton btn_createaccount { get; set; }


        [Outlet]
        UIKit.UIButton btn_login { get; set; }


        [Outlet]
        UIKit.UIImageView img_logo { get; set; }


        [Outlet]
        UIKit.UILabel lbl_already { get; set; }


        [Outlet]
        UIKit.UIView view_circle { get; set; }


        [Outlet]
        UIKit.UIButton view_point1 { get; set; }


        [Outlet]
        UIKit.UIButton view_point2 { get; set; }


        [Outlet]
        UIKit.UIButton view_point3 { get; set; }


        [Outlet]
        UIKit.UIView view_points { get; set; }


        [Outlet]
        UIKit.UIButton view_poitn4 { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btn_createaccount != null) {
                btn_createaccount.Dispose ();
                btn_createaccount = null;
            }

            if (btn_login != null) {
                btn_login.Dispose ();
                btn_login = null;
            }

            if (img_logo != null) {
                img_logo.Dispose ();
                img_logo = null;
            }

            if (lbl_already != null) {
                lbl_already.Dispose ();
                lbl_already = null;
            }

            if (view_circle != null) {
                view_circle.Dispose ();
                view_circle = null;
            }

            if (view_point1 != null) {
                view_point1.Dispose ();
                view_point1 = null;
            }

            if (view_point2 != null) {
                view_point2.Dispose ();
                view_point2 = null;
            }

            if (view_point3 != null) {
                view_point3.Dispose ();
                view_point3 = null;
            }

            if (view_points != null) {
                view_points.Dispose ();
                view_points = null;
            }

            if (view_poitn4 != null) {
                view_poitn4.Dispose ();
                view_poitn4 = null;
            }
        }
    }
}