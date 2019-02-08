using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace CABASUS
{
    public partial class TransitionSegue : UIStoryboardSegue
    {
        public TransitionSegue (IntPtr handle) : base (handle)
        {
        }

        public override void Perform()
        {
            //base.Perform();
            if (SourceViewController.ChildViewControllers.Length > 0) 
            {
                SwapFromViewController(SourceViewController.ChildViewControllers[0], DestinationViewController);
            }
            else
            {
                AddInitialViewController(DestinationViewController);
            }
        }

        private void SwapFromViewController(UIViewController fromViewcontroller, UIViewController toViewcontroller)
        {
            fromViewcontroller.WillMoveToParentViewController(null);
            toViewcontroller.View.Frame = SourceViewController.View.Bounds;

            SourceViewController.AddChildViewController(toViewcontroller);
            SourceViewController.Transition(fromViewcontroller,
            toViewcontroller,
                0.3,
            UIViewAnimationOptions.TransitionFlipFromLeft,
                () => {},(bool finished)=> {
                    fromViewcontroller.RemoveFromParentViewController();
                    toViewcontroller.DidMoveToParentViewController(SourceViewController);
                    var containerViewcontroller = SourceViewController as ITransitioningViewController;
                    if (containerViewcontroller != null) 
                    {
                        containerViewcontroller.ViewChangin.TrySetResult(true);
                    }
                });
        }

        private void AddInitialViewController(UIViewController viewController)
        {
            SourceViewController.AddChildViewController(viewController);
            viewController.View.Frame = SourceViewController.View.Bounds;
            SourceViewController.Add(viewController.View);
            viewController.DidMoveToParentViewController(SourceViewController);

            var containerViewController = SourceViewController as ITransitioningViewController;

            if (containerViewController != null) 
            {
                containerViewController.ViewChangin.TrySetResult(true);
            }
        }
    }

    public interface ITransitioningViewController
    {
        TaskCompletionSource<bool> ViewChangin { get; set; }
    }
}