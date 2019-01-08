using System.Collections.Generic;
using CABASUS.Controllers;
using CABASUS.Modelos;
using Foundation;
using UIKit;

namespace CABASUS.Adapters
{
    class Horses_Adapter_Delegate: UITableViewDelegate
    {
        UIViewController controlador;
        List<compartidos> listHorses;


        public Horses_Adapter_Delegate(UIViewController controller, List<compartidos> listHorses)
        {
            controlador = controller;
            this.listHorses = listHorses;
        }

        [Export("tableView:editActionsForRowAtIndexPath:")]
        public UITableViewRowAction[] EditActionsForRow(UITableView tableView, NSIndexPath indexPath)
        {
            var deleteAction = UITableViewRowAction.Create
            (
                UITableViewRowActionStyle.Destructive,
                "Delete",
                (arg1, arg2) => { 
            
            }
            );
            var editAction = UITableViewRowAction.Create
            (
                UITableViewRowActionStyle.Normal,
                "Edit",
                (arg1, arg2) => {
                    var detalle = controlador.Storyboard.InstantiateViewController("Register_Horse_ViewController") as Register_Horse_ViewController;
                    detalle.indicadorAccion = false;
                    detalle.id_horse = listHorses[arg2.Row].id_caballo;
                    detalle.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
                    detalle.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
                    controlador.PresentViewController(detalle, true, null);

                }
            );

            editAction.BackgroundColor = UIColor.Black;

            return new UITableViewRowAction[] { deleteAction, editAction };
        }
    }
}