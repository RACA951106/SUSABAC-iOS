using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using UIKit;

namespace CABASUS.Controllers
{
    public partial class Hamburguer_ViewController : UIViewController
    {
        private Hamburger_Container_ViewController containerViewController;
        
        public Hamburguer_ViewController(IntPtr handre) : base(handre)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            #region acomodar elementos de la interfaz;

            Scroll_H_Menu.Frame = new CGRect(0, 20, View.Frame.Width, View.Frame.Height);
            Scroll_H_Menu.ContentSize = new CGSize(View.Frame.Width + (View.Frame.Width / 3) * 2, 0);
            Scroll_H_Menu.SetContentOffset(new CGPoint((View.Frame.Width / 3) * 2, 0), false);
            //Scroll_H_Menu.ScrollEnabled= false;

            ViewToolbar.Frame = new CGRect((View.Frame.Width / 3) * 2, 0, View.Frame.Width, 50);

            btnOpenMenu.Frame = new CGRect(10, 10, ViewToolbar.Frame.Height - 20, ViewToolbar.Frame.Height - 20);
            //btnOpenMenu.BackgroundColor = UIColor.Red;
            btnOpenMenu.Tag = 0;

            lblNombrePantalla.Frame = new CGRect(btnOpenMenu.Frame.Width + 20, 10, ViewToolbar.Frame.Width - ((btnOpenMenu.Frame.Width + 20) * 2), ViewToolbar.Frame.Height - 20);
            lblNombrePantalla.TextAlignment = UITextAlignment.Center;

            btnNotification.Frame = new CGRect(lblNombrePantalla.Frame.Width + lblNombrePantalla.Frame.X + 10, 10, ViewToolbar.Frame.Height - 20, ViewToolbar.Frame.Height - 20);
            //btnNotification.BackgroundColor = UIColor.Brown;

            ViewMenu.Frame = new CGRect(0, 0, (View.Frame.Width / 3) * 2, View.Frame.Height);
            //ViewMenu.BackgroundColor = UIColor.Red;

            Content.Frame = new CGRect((View.Frame.Width / 3) * 2, 20 + ViewToolbar.Frame.Height, View.Frame.Width, View.Frame.Height - 20 - ViewToolbar.Frame.Height);

            imgUse.Frame = new CGRect(15, 15, 100, 100);
            imgUse.BackgroundColor = UIColor.Red;

            lblNombreUser.Frame = new CGRect(imgUse.Frame.Height + 20, (imgUse.Frame.Width + 15) / 2, ViewMenu.Frame.Width - imgUse.Frame.Width - 20, imgUse.Frame.Width / 2);
            lblNombreUser.Text = "Jose de Jesus Ricardo Santos";
            lblNombreUser.Lines = 2;

            ScrollMenu.Frame = new CGRect(0, imgUse.Frame.Width + 30, ViewMenu.Frame.Width, ViewMenu.Frame.Height - imgUse.Frame.Width - ViewToolbar.Frame.Height - 30 - 50);
            //ScrollMenu.BackgroundColor = UIColor.Gray;

            imglogo.Frame = new CGRect(0, ScrollMenu.Frame.Height + ScrollMenu.Frame.Y, ViewMenu.Frame.Width, View.Frame.Height - ScrollMenu.Frame.Y + ScrollMenu.Frame.Height);
            imglogo.BackgroundColor = UIColor.Black;

            btnProfile.Frame = new CGRect(imgUse.Frame.X, 5, ScrollMenu.Frame.Width - imgUse.Frame.X, ScrollMenu.Frame.Height / 9);
            btnHorses.Frame = new CGRect(imgUse.Frame.X, btnProfile.Frame.Height + btnProfile.Frame.Y + 1, ScrollMenu.Frame.Width - imgUse.Frame.X, ScrollMenu.Frame.Height / 9);
            btnDiary.Frame = new CGRect(imgUse.Frame.X, btnHorses.Frame.Height + btnHorses.Frame.Y + 1, ScrollMenu.Frame.Width - imgUse.Frame.X, ScrollMenu.Frame.Height / 9);
            btnActivities.Frame = new CGRect(imgUse.Frame.X, btnDiary.Frame.Height + btnDiary.Frame.Y + 1, ScrollMenu.Frame.Width - imgUse.Frame.X, ScrollMenu.Frame.Height / 9);
            btnCalendar.Frame = new CGRect(imgUse.Frame.X, btnActivities.Frame.Height + btnActivities.Frame.Y + 1, ScrollMenu.Frame.Width - imgUse.Frame.X, ScrollMenu.Frame.Height / 9);
            btnHealt.Frame = new CGRect(imgUse.Frame.X, btnCalendar.Frame.Height + btnCalendar.Frame.Y + 1, ScrollMenu.Frame.Width - imgUse.Frame.X, ScrollMenu.Frame.Height / 9);
            btnSettings.Frame = new CGRect(imgUse.Frame.X, btnHealt.Frame.Height + btnHealt.Frame.Y + 1, ScrollMenu.Frame.Width - imgUse.Frame.X, ScrollMenu.Frame.Height / 9);
            btnChat.Frame = new CGRect(imgUse.Frame.X, btnSettings.Frame.Height + btnSettings.Frame.Y + 1, ScrollMenu.Frame.Width - imgUse.Frame.X, ScrollMenu.Frame.Height / 9);
            btnlogout.Frame = new CGRect(imgUse.Frame.X, btnChat.Frame.Height + btnChat.Frame.Y + 1, ScrollMenu.Frame.Width - imgUse.Frame.X, ScrollMenu.Frame.Height / 9);

            ScrollMenu.ContentSize = new CGSize(ViewMenu.Frame.Width, ScrollMenu.Frame.Height + 20);

            //btnProfile.BackgroundColor = UIColor.Green;
            //btnHorses.BackgroundColor = UIColor.Red;
            //btnDiary.BackgroundColor = UIColor.Blue;
            //btnActivities.BackgroundColor = UIColor.Green;
            //btnCalendar.BackgroundColor = UIColor.Red;
            //btnHealt.BackgroundColor = UIColor.Blue;
            //btnSettings.BackgroundColor = UIColor.Green;
            //btnChat.BackgroundColor = UIColor.Red;
            //btnlogout.BackgroundColor = UIColor.Blue;

            #endregion;

            #region Efecto Menu

            Scroll_H_Menu.DraggingEnded+=delegate {
                if (Scroll_H_Menu.ContentOffset.X < ((View.Frame.Width / 3) * 2) - 80)
                    Scroll_H_Menu.SetContentOffset(new CGPoint(0, 0), true);
                else
                {
                    Scroll_H_Menu.SetContentOffset(new CGPoint((View.Frame.Width / 3) * 2, 0), true);
                    //Scroll_H_Menu.ScrollEnabled = false;
                }
            };

            Scroll_H_Menu.DecelerationStarted+=delegate {

                if (Scroll_H_Menu.ContentOffset.X < ((View.Frame.Width / 3) * 2) - 80)
                    Scroll_H_Menu.SetContentOffset(new CGPoint(0, 0), true);
                else
                {
                    Scroll_H_Menu.SetContentOffset(new CGPoint((View.Frame.Width / 3) * 2, 0), true);
                    //Scroll_H_Menu.ScrollEnabled = false;
                }
            };


            btnOpenMenu.TouchUpInside += delegate {

                if (btnOpenMenu.Tag == 0)
                {
                    Scroll_H_Menu.SetContentOffset(new CGPoint((View.Frame.Width / 3) * 2, 0), true);
                    btnOpenMenu.Tag = 1;
                }
                else
                {
                    Scroll_H_Menu.SetContentOffset(new CGPoint(0, 0), true);
                    btnOpenMenu.Tag = 0;
                }
            };

            #endregion

            #region abrir pantallas con los botones;

            btnHorses.TouchUpInside += delegate
            {
                PresentContainerView(0);
            };
            btnChat.TouchUpInside += delegate
            {
                PresentContainerView(1);
            };

            #endregion;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        async void PresentContainerView(nint selectedId)
        {
            //we need some synchronisation because the new view controller
            //is animated in. Disable the switch until the animation is complete

            switch (selectedId)
            {
                case 0:
                    await containerViewController.PresentHorseAsync();
                    break;
                case 1:
                    await containerViewController.PresentChatAsync();
                    break;
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            //base.PrepareForSegue(segue, sender);
            if (segue.Identifier == "Contenedor_Vistas") 
            {
                containerViewController = segue.DestinationViewController as Hamburger_Container_ViewController;
            }
        }
    }
}

