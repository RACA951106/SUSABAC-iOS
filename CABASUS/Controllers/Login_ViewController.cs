using System;
using CoreGraphics;
using UIKit;
using Foundation;
using AVFoundation;
using AVKit;
using System.Collections.Generic;
using Xamarin.iOS.iCarouselBinding;
using System.Timers;
using System.Drawing;

namespace CABASUS.Controllers
{
    public partial class Login_ViewController : UIViewController
    {
        #region Notificacion de video
        AVAsset videoAsset;
        AVPlayerItem videoPlayerItem;
        AVPlayer videoPlayer;
        AVPlayerLayer videoPlayerLayer;
        NSObject videoEndNotificationToken;
        #endregion

        #region Carrousel
        UISwipeGestureRecognizer gestor = new UISwipeGestureRecognizer();
        UISwipeGestureRecognizer gestol = new UISwipeGestureRecognizer();
        private List<string> items;
        #endregion
        public Login_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            #region Video de fondo           
            videoAsset = AVAsset.FromUrl(NSUrl.FromFilename("Video.mp4"));
            videoPlayerItem = new AVPlayerItem(videoAsset);
            videoPlayer = new AVPlayer(videoPlayerItem);
            videoPlayerLayer = AVPlayerLayer.FromPlayer(videoPlayer);
            videoPlayerLayer.VideoGravity = AVLayerVideoGravity.ResizeAspectFill;
            videoPlayerLayer.Frame = View.Frame;
            View.Layer.InsertSublayer(videoPlayerLayer, 0);
            videoPlayer.Play();
            videoPlayer.Volume = 0;

            // Subscribe to video end notification
            videoPlayer.ActionAtItemEnd = AVPlayerActionAtItemEnd.None;
            videoEndNotificationToken = NSNotificationCenter.DefaultCenter.AddObserver(AVPlayerItem.DidPlayToEndTimeNotification, VideoDidFinishPlaying, videoPlayerItem);

            #endregion

            #region Agregar logo

            img_logo.Frame = new CGRect((View.Frame.Width / 2) - ((View.Frame.Width - 50) / 2), 100, View.Frame.Width - 50, 100);
            img_logo.ContentMode = UIViewContentMode.ScaleAspectFit;

            #endregion

            #region Ubicacion y tamaño boton create account

            btn_createaccount.Frame = new CGRect(50, View.Frame.Height - 200, View.Frame.Width - 100, 35);
            btn_createaccount.Layer.CornerRadius = 15f;
            btn_createaccount.ClipsToBounds = true;

            #endregion

            #region Ubicacion y tamaño label already a user

            lbl_already.Frame = new CGRect(0, View.Frame.Height - 145, View.Frame.Width, 12);
            lbl_already.TextAlignment = UITextAlignment.Center;
            lbl_already.TextColor = UIColor.White;
            lbl_already.Font = UIFont.BoldSystemFontOfSize(11f);
            #endregion

            #region Ubicacion y tamaño boton log in

            btn_login.Frame = new CGRect(50, View.Frame.Height - 130, View.Frame.Width - 100, 35);
            btn_login.Layer.CornerRadius = 15f;
            btn_login.ClipsToBounds = true;

            #endregion

            #region Carrousel

            gestor.Direction = UISwipeGestureRecognizerDirection.Right;
            gestol.Direction = UISwipeGestureRecognizerDirection.Left;

            view_circle.Frame = new CGRect(0, (View.Frame.Height/2)-75, View.Frame.Width, 150);
            view_circle.BackgroundColor = UIColor.Clear;


            items = new List<string>();
            items.Add("Welcome");
            items.Add("Share horses");
            items.Add("Run activities");
            items.Add("Diary & Journals");


            // Setup iCarousel view
            var carousel = new iCarousel
            {
                //Bounds = new CGRect(0,0,view_circle.Frame.Width,view_circle.Frame.Height),
                ContentMode = UIViewContentMode.Center,
                Type = iCarouselType.Linear,
                Frame = new CGRect(0, 0, view_circle.Frame.Width, view_circle.Frame.Height),
                CenterItemWhenSelected = true,
                DataSource = new SimpleDataSource(items, View.Frame.Width),
                Delegate = new SimpleDelegate(this)
            };
            carousel.IgnorePerpendicularSwipes = true;

            var view = new UIView(new CGRect(0, 0, view_circle.Frame.Width, view_circle.Frame.Height));
            view_circle.AddSubviews(carousel, view);
            ViewDidLayoutSubviews();

            gestor.AddTarget(() => HandleSwiper(gestor, carousel));
            gestol.AddTarget(() => HandleSwiper(gestol, carousel));
            view.AddGestureRecognizer(gestol);
            view.AddGestureRecognizer(gestor);

           Timer t = new Timer();
            t.Interval = 3500;
            t.Enabled = true;
            t.Elapsed += (s, e) =>
            {
                InvokeOnMainThread(() =>
                {
                    carousel.ScrollToItemAtIndex(carousel.CurrentItemIndex + 1, true);
                    Cambiarestadoboton((int) carousel.CurrentItemIndex + 1);
                });
            };
            t.Start();
            #endregion

            #region Puntos inferiores;
            view_points.Frame = new CGRect((View.Frame.Width / 2) - 30, (View.Frame.Height / 2) + 75, 60, 20);
            view_points.BackgroundColor = UIColor.Clear;

            view_point1.Frame = new CGRect(0, 5, 8, 8);
            view_point1.Layer.CornerRadius = 4f;
            view_point1.ClipsToBounds = true;
            view_point1.BackgroundColor = UIColor.White;
            view_point1.TouchUpInside+=delegate {
                carousel.ScrollToItemAtIndex(0, true);
                Cambiarestadoboton(0);
            };

            view_point2.Frame = new CGRect(15, 5, 8, 8);
            view_point2.Layer.CornerRadius = 4f;
            view_point2.ClipsToBounds = true;
            view_point2.BackgroundColor = UIColor.White;
            view_point2.TouchUpInside += delegate {
                carousel.ScrollToItemAtIndex(1, true);
                Cambiarestadoboton(1);
            };

            view_point3.Frame = new CGRect(30, 5, 8, 8);
            view_point3.Layer.CornerRadius = 4f;
            view_point3.ClipsToBounds = true;
            view_point3.BackgroundColor = UIColor.White;
            view_point3.TouchUpInside += delegate {
                carousel.ScrollToItemAtIndex(2, true);
                Cambiarestadoboton(2);
            };

            view_poitn4.Frame = new CGRect(45, 5, 8, 8);
            view_poitn4.Layer.CornerRadius = 4f;
            view_poitn4.ClipsToBounds = true;
            view_poitn4.BackgroundColor = UIColor.White;
            view_poitn4.TouchUpInside += delegate {
                carousel.ScrollToItemAtIndex(3, true);
                Cambiarestadoboton(3);
            };

            #endregion

        }

        public void Cambiarestadoboton(int numero)
        {
            switch(numero){
                case 0:
                    view_point1.BackgroundColor = UIColor.White;
                    view_point1.Alpha = 1f;
                    view_point2.Alpha = .5f;
                    view_point3.Alpha = .5f;
                    view_poitn4.Alpha = .5f;
                    break;
                case 1:
                    view_point1.Alpha = .5f;
                    view_point2.BackgroundColor = UIColor.White;
                    view_point2.Alpha = 1f;
                    view_point3.Alpha = .5f;
                    view_poitn4.Alpha = .5f;
                    break;
                case 2:
                    view_point1.Alpha = .5f;
                    view_point2.Alpha = .5f;
                    view_point3.BackgroundColor = UIColor.White;
                    view_point3.Alpha = 1f;
                    view_poitn4.Alpha = .5f;
                    break;
                case 3:
                    view_point1.Alpha = .5f;
                    view_point2.Alpha = .5f;
                    view_point3.Alpha = .5f;
                    view_poitn4.BackgroundColor = UIColor.White;
                    view_poitn4.Alpha = 1f;
                    break;
                case 4:
                    view_point1.BackgroundColor = UIColor.White;
                    view_point1.Alpha = 1f;
                    view_point2.Alpha = .5f;
                    view_point3.Alpha = .5f;
                    view_poitn4.Alpha = .5f;
                    break;
                case-1:
                    view_point1.Alpha = .5f;
                    view_point2.Alpha = .5f;
                    view_point3.Alpha = .5f;
                    view_poitn4.BackgroundColor = UIColor.White;
                    view_poitn4.Alpha = 1f;
                    break;
            }

               


        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

        }

        private void VideoDidFinishPlaying(NSNotification obj)
        {
           // Console.WriteLine("Video Finished, will now restart");
            videoPlayer.Seek(new CoreMedia.CMTime(0, 1));
        }

        void HandleSwiper(UISwipeGestureRecognizer sender, iCarousel carrusel)
        {
            if (sender.Direction == UISwipeGestureRecognizerDirection.Right)
            {
                carrusel.ScrollToItemAtIndex(carrusel.CurrentItemIndex - 1, true);
                Cambiarestadoboton((int)carrusel.CurrentItemIndex - 1);

            }
            if (sender.Direction == UISwipeGestureRecognizerDirection.Left)
            {
                carrusel.ScrollToItemAtIndex(carrusel.CurrentItemIndex + 1, true);
                Cambiarestadoboton((int)carrusel.CurrentItemIndex + 1);
            }
        }

        public class SimpleDataSource : iCarouselDataSource
        {
            private readonly List<string> _data;
            nfloat _medidaancho;

            public SimpleDataSource(List<string> data,nfloat medidaancho)
            {
                _data = data;
                _medidaancho = medidaancho;
            }

            public override nint NumberOfItemsInCarousel(iCarousel carousel) => _data.Count;

            public override UIView ViewForItemAtIndex(iCarousel carousel, nint index, UIView view)
            {
                UILabel label;

                // create new view if no view is available for recycling
                if (view == null)
                {
                    label = new UILabel(new CGRect(0, 0, _medidaancho,150))
                    {
                        BackgroundColor = UIColor.Clear,
                        TextColor=UIColor.White,
                        TextAlignment = UITextAlignment.Center,
                        Tag = 1
                       
                    };
                    view = label;
                }
                else
                {
                    // get a reference to the label in the recycled view
                    label = (UILabel)view.ViewWithTag(1);
                }

                label.Text = _data[(int)index].ToString();

                return view;
            }
        }

        public class SimpleDelegate : iCarouselDelegate
        {
            private readonly Login_ViewController _viewController;

            public SimpleDelegate(Login_ViewController vc)
            {
                _viewController = vc;
            }

            public override nfloat ValueForOption(iCarousel carousel, iCarouselOption option, nfloat value)
            {
                switch (option)
                {
                    case iCarouselOption.Wrap:
                        return 1;
                    default:
                        return value;
                }
            }

            public override void CarouselDidEndScrollingAnimation(iCarousel carousel)
            {
                //se ejecuta al terminar la animacion 
            }

            public override void DidSelectItemAtIndex(iCarousel carousel, nint index)
            {
                // se ejecuta al hacer clic
            }

        }
    }
}

