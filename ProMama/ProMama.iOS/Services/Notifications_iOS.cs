using Foundation;
using ProMama.iOS.Services;
using ProMama.ViewModels.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Notifications_iOS))]
namespace ProMama.iOS.Services
{
    class Notifications_iOS : INotificationService
    {
        public Notifications_iOS() { }

        public void Notify(string titulo, string texto, int dias)
        {
            // create the notification
            var notification = new UILocalNotification();

            // set the fire date (the date time in which it will fire)
            notification.FireDate = NSDate.FromTimeIntervalSinceNow(86400 * dias);

            // configure the alert
            notification.AlertTitle = titulo;
            notification.AlertAction = "Ver";
            notification.AlertBody = texto;

            // modify the badge
            notification.ApplicationIconBadgeNumber = 1;

            // set the sound to be the default sound
            notification.SoundName = UILocalNotification.DefaultSoundName;

            // schedule it
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }
    }
}