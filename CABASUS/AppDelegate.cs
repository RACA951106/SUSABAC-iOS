﻿using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Firebase.CloudMessaging;
using Firebase.Core;
using Foundation;
using UIKit;
using UserNotifications;

namespace CABASUS
{
    [Register("AppDelegate")]     public class AppDelegate : UIApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate     {                   public event EventHandler<UserInfoEventArgs> MessageReceived;          // class-level declarations          public override UIWindow Window         {             get;             set;         }          public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)         {             //GetLocalFilePath("Chat.sqlite", "Chat", "db");             //var stringCone2 = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Chat.sqlite");             //var c2 = new SQLiteConnection(stringCone2);               //c2.Query<Conversacion>("DELETE FROM Conversacion", new Conversacion().usuario);               // Override point for customization after application launch.             // If not required for your application you can safely delete this method              //  (Window.RootViewController as UINavigationController).PushViewController(new UserInfoViewController(this), true);             UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;              App.Configure();              // Register your app for remote notifications.             if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))             {                 // For iOS 10 display notification (sent via APNS)                 UNUserNotificationCenter.Current.Delegate = this;                  var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;                 UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {                     Console.WriteLine(granted);                 });             }             else             {                 // iOS 9 or before                 var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;                 var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);                 UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);             }              UIApplication.SharedApplication.RegisterForRemoteNotifications();              Messaging.SharedInstance.Delegate = this;              // To connect with FCM. FCM manages the connection, closing it             // when your app goes into the background and reopening it              // whenever the app is foregrounded.             Messaging.SharedInstance.ShouldEstablishDirectChannel = true;              return true;         }          [Export("messaging:didReceiveRegistrationToken:")]         public async void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)         {             // Monitor token generation: To be notified whenever the token is updated.              LogInformation(nameof(DidReceiveRegistrationToken), $"Firebase registration token: { fcmToken}");


            var Ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "token.xml");

            if (File.Exists(Ruta))
            {
                try
                {
                    string server = "http://192.168.1.74:5001/api/Usuario/actualizarTokenFB?tokenFB=" + fcmToken + "&id_dispositivo=" + UIDevice.CurrentDevice.IdentifierForVendor.AsString();
                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new ShareInSide().consultxmlToken().token);

                    var respuesta = await cliente.GetAsync(server);
                    var content = await respuesta.Content.ReadAsStringAsync();

                    respuesta.EnsureSuccessStatusCode();
                    if (respuesta.IsSuccessStatusCode)
                        Console.WriteLine("Token Actualizado");
                    else
                        Console.WriteLine(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }          // You'll need this method if you set "FirebaseAppDelegateProxyEnabled": NO in GoogleService-Info.plist         public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)         {           Messaging.SharedInstance.ApnsToken = deviceToken;         }          public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)         {             // Handle Notification messages in the background and foreground.             // Handle Data messages for iOS 9 and below.              // If you are receiving a notification message while your app is in the background,             // this callback will not be fired till the user taps on the notification launching the application.             // TODO: Handle data of notification              // With swizzling disabled you must let Messaging know about the message, for Analytics             //Messaging.SharedInstance.AppDidReceiveMessage (userInfo);              HandleMessage(userInfo);              // Print full message.             LogInformation(nameof(DidReceiveRemoteNotification), userInfo);              completionHandler(UIBackgroundFetchResult.NewData);              //NSObject cuerpo;              //userInfo.TryGetValue(new NSString("gcm.notification.data"), out cuerpo);              //var cosas = JsonConvert.DeserializeObject<Mensaje>(cuerpo.ToString());              //var stringCone = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Chat.sqlite");             //var c = new SQLiteConnection(stringCone);              //c.Query<Conversacion>("INSERT INTO Conversacion VALUES(null,1,'" + cosas.men + "', '" + cosas.id + "')", new Conversacion().usuario);              //c.Close();              NSNotificationCenter.DefaultCenter.PostNotificationName("MensajeNuevo", this);          }          [Export("messaging:didReceiveMessage:")]         public void DidReceiveMessage(Messaging messaging, RemoteMessage remoteMessage)         {             //Information:             //{             //    "collapse_key" = "com.Germany.Cabasus.notificacionesfirebase";             //    from = 386368652630;             //    notification =     {             //        body = "{\"men\":\"Holi\",\"id\":\"3OWtTU6PIkmrUk8r9oqz\"}";             //        e = 1;             //        title = "Mensaje para Pegasus";             //    };             //}              // Handle Data messages for iOS 10 and above.              HandleMessage(remoteMessage.AppData);              LogInformation(nameof(DidReceiveMessage), remoteMessage.AppData);              //NSError error;             //var json = NSJsonSerialization.Serialize(remoteMessage.AppData, NSJsonWritingOptions.PrettyPrinted, out error);              //var objeto = JsonConvert.DeserializeObject<RootObject>(json.ToString(NSStringEncoding.UTF8));              //var stringCone = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Chat.sqlite");             //var c = new SQLiteConnection(stringCone);              //c.Query<Conversacion>("INSERT INTO Conversacion VALUES(null,1,'" + objeto.men + "', '" + objeto.id + "')", new Conversacion().usuario);              //c.Close();              //NSNotificationCenter.DefaultCenter.PostNotificationName("MensajeNuevo", this);         }          void HandleMessage(NSDictionary message)         {             if (MessageReceived == null)                 return;              MessageType messageType;             if (message.ContainsKey(new NSString("aps")))                 messageType = MessageType.Notification;             else                 messageType = MessageType.Data;              var e = new UserInfoEventArgs(message, messageType);             MessageReceived(this, e);         }          public static void ShowMessage(string title, string message, UIViewController fromViewController, Action actionForOk = null)         {             var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);             alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (obj) => actionForOk?.Invoke()));             fromViewController.PresentViewController(alert, true, null);         }          void LogInformation(string methodName, object information) => Console.WriteLine($"\nMethod name: { methodName}\nInformation: { information}");          //public static string GetLocalFilePath(string filename, string nombreenresources, string tipoenresources)         //{         //    string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);         //    string dbPath = Path.Combine(docFolder, filename);         //    CopyDatabaseIfNotExists(dbPath, nombreenresources, tipoenresources);          //    return dbPath;         //}          //private static void CopyDatabaseIfNotExists(string dbPath, string nameinres, string typeinres)         //{         //    if (!File.Exists(dbPath))         //    {         //        var existingDb = NSBundle.MainBundle.PathForResource(nameinres, typeinres);         //        File.Copy(existingDb, dbPath);         //    }         //}      }      public class UserInfoEventArgs : EventArgs     {         public NSDictionary UserInfo { get; private set; }         public MessageType MessageType { get; private set; }          public UserInfoEventArgs(NSDictionary userInfo, MessageType messageType)         {             UserInfo = userInfo;             MessageType = messageType;         }     }      public enum MessageType     {         Notification,         Data     }      //public class Mensaje     //{     //    public string id { get; set; }     //    public string men { get; set; }     //} 
}

