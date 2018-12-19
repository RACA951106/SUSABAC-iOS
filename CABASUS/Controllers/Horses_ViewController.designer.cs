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
	[Register ("Horses_ViewController")]
	partial class Horses_ViewController
	{
		[Outlet]
		UIKit.UIButton btnAgregar { get; set; }

		[Outlet]
		UIKit.UIImageView imgIconoBuscador { get; set; }

		[Outlet]
		UIKit.UITableView tableCaballos { get; set; }

		[Outlet]
		UIKit.UITextField txtBuscador { get; set; }

		[Outlet]
		UIKit.UIView viewBuscadorFondo { get; set; }

		[Outlet]
		UIKit.UIView viewLinea { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAgregar != null) {
				btnAgregar.Dispose ();
				btnAgregar = null;
			}

			if (imgIconoBuscador != null) {
				imgIconoBuscador.Dispose ();
				imgIconoBuscador = null;
			}

			if (tableCaballos != null) {
				tableCaballos.Dispose ();
				tableCaballos = null;
			}

			if (txtBuscador != null) {
				txtBuscador.Dispose ();
				txtBuscador = null;
			}

			if (viewBuscadorFondo != null) {
				viewBuscadorFondo.Dispose ();
				viewBuscadorFondo = null;
			}

			if (viewLinea != null) {
				viewLinea.Dispose ();
				viewLinea = null;
			}
		}
	}
}
