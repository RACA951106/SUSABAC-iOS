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