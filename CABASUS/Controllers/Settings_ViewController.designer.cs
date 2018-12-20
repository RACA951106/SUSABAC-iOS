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
	[Register ("Settings_ViewController")]
	partial class Settings_ViewController
	{
		[Outlet]
		UIKit.UITableView listaSettings { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (listaSettings != null) {
				listaSettings.Dispose ();
				listaSettings = null;
			}
		}
	}
}
