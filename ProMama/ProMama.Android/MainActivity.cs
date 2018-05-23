using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Droid;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.Iconize;
// Media
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

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
            ImageCircleRenderer.Init();

            // FFImageLoading
            CachedImageRenderer.Init(true);

            // Iconize
            Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());

            // Acr.UserDialogs
            UserDialogs.Init(this);

            LoadApplication(new ProMama.App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

