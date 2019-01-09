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
	[Register ("Chat_Controller")]
	partial class Chat_Controller
	{
		[Outlet]
		UIKit.UIButton btnEnviarMensaje { get; set; }

		[Outlet]
		UIKit.UITableView tableChat { get; set; }

		[Outlet]
		UIKit.UITextField txtMensaje { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnEnviarMensaje != null) {
				btnEnviarMensaje.Dispose ();
				btnEnviarMensaje = null;
			}

			if (tableChat != null) {
				tableChat.Dispose ();
				tableChat = null;
			}

			if (txtMensaje != null) {
				txtMensaje.Dispose ();
				txtMensaje = null;
			}
		}
	}
}
