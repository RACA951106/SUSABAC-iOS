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

			if (txt_email != null) {
				txt_email.Dispose ();
				txt_email = null;
			}

			if (txt_password != null) {
				txt_password.Dispose ();
				txt_password = null;
			}

			if (btnback != null) {
				btnback.Dispose ();
				btnback = null;
			}
		}
	}
}
