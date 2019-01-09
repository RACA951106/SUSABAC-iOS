// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CABASUS.Adapters
{
    [Register ("Custom_Cell_Chat_Adapter")]
    partial class Custom_Cell_Chat_Adapter
    {
        [Outlet]
        UIKit.NSLayoutConstraint constLeft { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint constRight { get; set; }


        [Outlet]
        UIKit.UILabel lblMensaje { get; set; }


        [Outlet]
        UIKit.UIView viewMensaje { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (constLeft != null) {
                constLeft.Dispose ();
                constLeft = null;
            }

            if (constRight != null) {
                constRight.Dispose ();
                constRight = null;
            }

            if (lblMensaje != null) {
                lblMensaje.Dispose ();
                lblMensaje = null;
            }

            if (viewMensaje != null) {
                viewMensaje.Dispose ();
                viewMensaje = null;
            }
        }
    }
}