using Foundation;
using UIKit;

namespace CABASUS.Adapters
{
    class Horses_Adapter_Delegate: UITableViewDelegate
    {
        [Export("tableView:editActionsForRowAtIndexPath:")]
        public UITableViewRowAction[] EditActionsForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var deleteAction = UITableViewRowAction.Create
            (
                UITableViewRowActionStyle.Destructive,
                "Delete",
                (arg1, arg2) => { }
            );
            var editAction = UITableViewRowAction.Create
            (
                UITableViewRowActionStyle.Normal,
                "Edit",
                (arg1, arg2) => { }
            );

            editAction.BackgroundColor = UIColor.Black;

            return new UITableViewRowAction[] { deleteAction, editAction };
        }
    }
}