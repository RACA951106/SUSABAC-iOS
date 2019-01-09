// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
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
			if (viewMensaje != null) {
				viewMensaje.Dispose ();
				viewMensaje = null;
			}

			if (lblMensaje != null) {
				lblMensaje.Dispose ();
				lblMensaje = null;
			}

			if (constLeft != null) {
				constLeft.Dispose ();
				constLeft = null;
			}

			if (constRight != null) {
				constRight.Dispose ();
				constRight = null;
			}
		}
	}
}
