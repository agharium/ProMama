using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using System.Threading.Tasks;

namespace ProMama.Droid
{
    [Activity(Label = "Pró-Mamá", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            // CarouselView
            var cv = typeof(Xamarin.Forms.CarouselView);
            var assembly = System.Reflection.Assembly.Load(cv.FullName);

            // ImageCircle
            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();

            // FFImageLoading
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            // Iconize
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule());

            // Acr.UserDialogs
            Acr.UserDialogs.UserDialogs.Init(this);

            // Notification Icon
            Plugin.LocalNotifications.LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.notification;

            LoadApplication(new ProMama.App());

            //Log.WriteLine(LogPriority.Debug, "BOOT PERMISSION", Application.Context.CheckSelfPermission("RECEIVE_BOOT_COMPLETED").ToString());

            //await GetReceivedBootCompletePermission();
        }

        /*async Task GetReceivedBootCompletePermission()
        {
            string[] BootPermission = { Manifest.Permission.ReceiveBootCompleted };

            if (CheckSelfPermission(BootPermission[0]) != (int)Permission.Granted && (int)Build.VERSION.SdkInt >= 23)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Permissão de inicialização");
                alert.SetMessage("O aplicativo precisa da permissão de inicialização para enviar notificações corretamente.");
                Dialog dialog = alert.Create();
                dialog.Show();

                RequestPermissions(BootPermission, 0);
            }
        }*/

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

