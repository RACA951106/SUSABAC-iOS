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
	[Register ("Search_ViewController")]
	partial class Search_ViewController
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UICollectionView collection { get; set; }

		[Outlet]
		UIKit.UIImageView imgIconoBuscadorFondo { get; set; }

		[Outlet]
		UIKit.UITextField txtBuscar { get; set; }

		[Outlet]
		UIKit.UIView viewBuscadorFondo { get; set; }

		[Outlet]
		UIKit.UIView viewLinea { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (collection != null) {
				collection.Dispose ();
				collection = null;
			}

			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (imgIconoBuscadorFondo != null) {
				imgIconoBuscadorFondo.Dispose ();
				imgIconoBuscadorFondo = null;
			}

			if (txtBuscar != null) {
				txtBuscar.Dispose ();
				txtBuscar = null;
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
