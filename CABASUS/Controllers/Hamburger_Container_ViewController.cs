using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace CABASUS
{
    public partial class Hamburger_Container_ViewController : UIViewController, ITransitioningViewController
    {
        private NSString IHorse = (NSString)"Horses";
        private NSString IChat = (NSString)"Chat";

        private TaskCompletionSource<bool> viewChangin;

        public Hamburger_Container_ViewController (IntPtr handle) : base (handle)
        {
        }

        public TaskCompletionSource<bool> ViewChangin 
        {
            get { return viewChangin; }
            set { viewChangin = value; }
        }

        public Task<bool> PresentHorseAsync()
        {
            ViewChangin = new TaskCompletionSource<bool>();
            PerformSegue(IHorse, this);
            return viewChangin.Task;
        }

        public Task<bool> PresentChatAsync()
        {
            ViewChangin = new TaskCompletionSource<bool>();
            PerformSegue(IChat, this);
            return viewChangin.Task;
        }
    }
}