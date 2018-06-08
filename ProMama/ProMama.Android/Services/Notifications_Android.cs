using Android.App;
using Android.Content;
using Java.Lang;
using ProMama.Droid.Services;
using ProMama.ViewModels.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Notifications_Android))]
namespace ProMama.Droid.Services
{
    class Notifications_Android : INotificationService
    {
        public const string PROMAMA_CHANNEL = "br.gov.rs.osorio.promama";

        public Notifications_Android() { }

        public void Notify(string titulo, string texto, int dias)
        {
            var Context = Android.App.Application.Context;

            // Get the notification manager:
            NotificationManager notificationManager = Context.GetSystemService(Context.NotificationService) as NotificationManager;

            var intent = Android.App.Application.Context.PackageManager.GetLaunchIntentForPackage(Context.PackageName);
            intent.AddFlags(ActivityFlags.ClearTop);

            var pendingIntent = PendingIntent.GetActivity(Context, 0, intent, PendingIntentFlags.UpdateCurrent);

            Notification.Builder builder;
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(PROMAMA_CHANNEL, "Notificações Pró-Mamá", NotificationImportance.Default)
                {
                    LockscreenVisibility = NotificationVisibility.Public
                };

                notificationManager.CreateNotificationChannel(channel);

                builder = new Notification.Builder(Context, PROMAMA_CHANNEL);
            } else
            {
                builder = new Notification.Builder(Context);
            }

            // Instantiate the builder and set notification elements:
            builder
                .SetContentTitle(titulo)
                .SetContentText(texto)
                .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentIntent(pendingIntent)
                .SetWhen(JavaSystem.CurrentTimeMillis() + (long)(8.64e+7 * dias));

            // Build the notification:
            Notification notification = builder.Build();

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}