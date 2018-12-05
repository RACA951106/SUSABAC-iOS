using System;
using AVFoundation;
using CoreGraphics;
using Foundation;
using Photos;
using UIKit;

namespace CABASUS.Controllers
{
    public partial class GaleryCameraAccessController : UIViewController
    {
        public GaleryCameraAccessController()
        {

        }

        public GaleryCameraAccessController(IntPtr handle) : base(handle)
        {

        }

        public async void BringUpCamera(UIButton imgseleccion, nint redondeo)
        {
            var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
            if (authorizationStatus != AVAuthorizationStatus.Authorized)
            {
                var access = await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
                if (access)
                    GotAccessToCamera(imgseleccion, redondeo);
            }
            else
                GotAccessToCamera(imgseleccion, redondeo);
        }

        public void BringUpPhotoGallery(UIButton imgseleccion, nint redondeo)
        {
            var imagePicker = new UIImagePickerController { SourceType = UIImagePickerControllerSourceType.PhotoLibrary, MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary) };
            imagePicker.AllowsEditing = true;

            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }

            vc.PresentViewController(imagePicker, true, null);

            imagePicker.FinishedPickingMedia += (sender, e) =>
            {
                UIImage originalImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
                if (originalImage != null)
                {
                    //UIImage imagen = new UIImage();
                    //imagen = resampleImageToSize(originalImage, new CGSize(100, 100));

                    imgseleccion.SetImage(originalImage, UIControlState.Normal);
                    imgseleccion.ImageView.Layer.CornerRadius = redondeo;
                    imgseleccion.ImageView.Layer.MasksToBounds = true;
                    imgseleccion.Tag = 2;

                    PHPhotoLibrary.RequestAuthorization(status =>
                    {
                        switch (status)
                        {
                            case PHAuthorizationStatus.Restricted:
                            case PHAuthorizationStatus.Denied:
                                // nope you don't have permission
                                break;
                            case PHAuthorizationStatus.Authorized:

                                var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                                var directoryname = System.IO.Path.Combine(documentsDirectory, "FotosUsuario");
                                System.IO.Directory.CreateDirectory(directoryname);
                                string jpgFilename = System.IO.Path.Combine(directoryname, "FotoUsuario.jpg"); // hardcoded filename, overwritten each time. You can make it dynamic as per your requirement.

                                NSData imgData = originalImage.AsJPEG();
                                NSError err = null;
                                if (imgData.Save(jpgFilename, false, out err))
                                {
                                    Console.WriteLine("saved as " + jpgFilename);
                                }
                                else
                                {
                                    Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
                                }

                                break;
                        }
                    });

                }
                this.BeginInvokeOnMainThread(() =>
                {
                    vc.DismissViewController(true, null);
                });
            };

            imagePicker.Canceled += (sender, e) => vc.DismissViewController(true, null);
        }

        private void GotAccessToCamera(UIButton imgseleccion, nint redondeo)
        {
            var imagePicker = new UIImagePickerController { SourceType = UIImagePickerControllerSourceType.Camera };
            imagePicker.AllowsEditing = true;

            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }

            vc.PresentViewController(imagePicker, true, null);

            imagePicker.FinishedPickingMedia += (sender, e) =>
            {
                UIImage originalImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
                if (originalImage != null)
                {
                    UIImage imagen = new UIImage();
                    imagen = resampleImageToSize(originalImage, new CGSize(100, 100));

                    imgseleccion.SetImage(imagen, UIControlState.Normal);
                    imgseleccion.ImageView.Layer.CornerRadius = redondeo;
                    imgseleccion.ImageView.Layer.MasksToBounds = true;
                    imgseleccion.Tag = 2;

                    PHPhotoLibrary.RequestAuthorization(status =>
                    {
                        switch (status)
                        {
                            case PHAuthorizationStatus.Restricted:
                            case PHAuthorizationStatus.Denied:
                                // nope you don't have permission
                                break;
                            case PHAuthorizationStatus.Authorized:

                                var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                                var directoryname = System.IO.Path.Combine(documentsDirectory, "FotosUsuario");
                                System.IO.Directory.CreateDirectory(directoryname);
                                string jpgFilename = System.IO.Path.Combine(directoryname, "FotoUsuario.jpg"); // hardcoded filename, overwritten each time. You can make it dynamic as per your requirement.

                                NSData imgData = originalImage.AsJPEG();
                                NSError err = null;
                                if (imgData.Save(jpgFilename, false, out err))
                                {
                                    Console.WriteLine("saved as " + jpgFilename);
                                }
                                else
                                {
                                    Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
                                }

                                break;
                        }
                    });
                }

                UIImage image = (UIImage)e.Info.ObjectForKey(new NSString("UIImagePickerControllerOriginalImage"));

                UIImage rotateImage = RotateImage(image, image.Orientation);

                rotateImage = rotateImage.Scale(new CGSize(rotateImage.Size.Width, rotateImage.Size.Height), 0.5f);

                var jpegImage = rotateImage.AsJPEG();

                this.BeginInvokeOnMainThread(() =>
                {
                    vc.DismissViewController(true, null);
                });
            };

            imagePicker.Canceled += (sender, e) => vc.DismissViewController(true, null);
        }

        double radians(double degrees) { return degrees * Math.PI / 180; }

        private UIImage RotateImage(UIImage src, UIImageOrientation orientation)
        {
            UIGraphics.BeginImageContext(src.Size);

            if (orientation == UIImageOrientation.Right)
                CGAffineTransform.MakeRotation((nfloat)radians(90));
            else if (orientation == UIImageOrientation.Left)
                CGAffineTransform.MakeRotation((nfloat)radians(-90));
            else if (orientation == UIImageOrientation.Down)
            {

            }
            else if (orientation == UIImageOrientation.Up)
                CGAffineTransform.MakeRotation((nfloat)radians(90));

            src.Draw(new CGPoint(0, 0));
            UIImage image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return image;
        }

        public UIImage resampleImageToSize(UIImage image, CGSize size)
        {
            nfloat originalWidth = image.Size.Width;
            nfloat originalHeight = image.Size.Height;
            nfloat originalRatio = originalWidth / originalHeight;

            nfloat targetRatio = size.Width / size.Height;

            var targetFrame = new CGRect(x: 0.0, y: 0.0, width: size.Width, height: size.Height);

            if (originalRatio > targetRatio)
            {

                nfloat targetHeight = size.Height;
                nfloat targetWidth = targetHeight * originalRatio;
                targetFrame = new CGRect(x: (size.Width - targetWidth) * 0.5, y: (size.Height - targetHeight) * 0.5, width: targetWidth, height: targetHeight);

            }
            else if (originalRatio < targetRatio)
            {
                nfloat targetWidth = size.Width;
                nfloat targetHeight = targetWidth / originalRatio;
                targetFrame = new CGRect(x: (size.Width - targetWidth) * 0.5, y: (size.Height - targetHeight) * 0.5, width: targetWidth, height: targetHeight);
            }
            UIGraphics.BeginImageContext(size);
            image.Draw(targetFrame);
            var outputImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return outputImage;
        }
    }
}

