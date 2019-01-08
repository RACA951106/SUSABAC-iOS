// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
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
			if (tableChat != null) {
				tableChat.Dispose ();
				tableChat = null;
			}

			if (viewFondoBuscador != null) {
				viewFondoBuscador.Dispose ();
				viewFondoBuscador = null;
			}

			if (txtBuscador != null) {
				txtBuscador.Dispose ();
				txtBuscador = null;
			}

			if (imgLupa != null) {
				imgLupa.Dispose ();
				imgLupa = null;
			}

			if (viewLineaBuscador != null) {
				viewLineaBuscador.Dispose ();
				viewLineaBuscador = null;
			}
		}
	}
}
