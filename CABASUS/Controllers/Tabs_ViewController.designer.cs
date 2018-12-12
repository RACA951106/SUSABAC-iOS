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
	[Register ("Tabs_ViewController")]
	partial class Tabs_ViewController
	{
		[Outlet]
		UIKit.UITabBar barra { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (barra != null) {
				barra.Dispose ();
				barra = null;
			}
		}
	}
}
