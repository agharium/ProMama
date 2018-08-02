using Foundation;
using UIKit;
using UserNotifications;

namespace ProMama.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();

            // CarouselView
            var cv = typeof(Xamarin.Forms.CarouselView);
            var assembly = System.Reflection.Assembly.Load(cv.FullName);

            // ImageCircle
            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();

            // FFImageLoading
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            // Notifications
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Ask the user for permission to get notifications on iOS 10.0+
                UNUserNotificationCenter.Current.RequestAuthorization(
                        UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
                        (approved, error) => { });
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                // Ask the user for permission to get notifications on iOS 8.0+
                var settings = UIUserNotificationSettings.GetSettingsForTypes(
                        UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                        new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            // styles
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(229, 57, 53);
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White });
            UINavigationBar.Appearance.BarStyle = UIBarStyle.Black;
            UIButton.Appearance.TintColor = UIColor.FromRGB(244, 67, 54);
            UIPickerView.Appearance.TintColor = UIColor.FromRGB(244, 67, 54);
            UIActionSheet.Appearance.TintColor = UIColor.FromRGB(244, 67, 54);
            UIAlertView.Appearance.TintColor = UIColor.FromRGB(244, 67, 54);
            UIDatePicker.Appearance.TintColor = UIColor.FromRGB(244, 67, 54);
            UIInputView.Appearance.TintColor = UIColor.FromRGB(244, 67, 54);
            UITabBar.Appearance.TintColor = UIColor.FromRGB(244, 67, 54);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
