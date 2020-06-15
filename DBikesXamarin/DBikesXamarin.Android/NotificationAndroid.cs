using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using DBikesXamarin.Droid;
using DBikesXamarin.Helpers.Notifications;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationAndroid))]
namespace DBikesXamarin.Droid
{
   public class NotificationAndroid : INotification
    {
        bool channelCreated = false;
        NotificationManager notificationManager;

        const string infoChannelId = "DBikesInfo";
        const string infoChannelName = "DublinBikes_StationInfo";
        const string infoChannelDescription = "DublinBikes station information.";
      
        int notification_id = 2003;

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)     {return;}
            notificationManager = (NotificationManager)Android.App.Application.Context.GetSystemService(
                Android.App.Application.NotificationService
                );
            var infoChannel = new NotificationChannel(infoChannelId, infoChannelName, NotificationImportance.Low)
                { Description = infoChannelDescription};
            notificationManager.CreateNotificationChannel(infoChannel);
            channelCreated = true;
        }

        public void Notify(string title, string message)
        {
            if (channelCreated == false)    {   CreateNotificationChannel();    }
            
            var b = new Notification.Builder(Android.App.Application.Context, infoChannelId)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.abc_ic_star_black_48dp)
                .SetLargeIcon(Android.Graphics.BitmapFactory.DecodeResource(
                    Android.App.Application.Context.Resources, Resource.Drawable.bikeicon)
                );
            
            // Make notification clickable to reopen app
            Android.Content.Intent intent = new Android.Content.Intent(Android.App.Application.Context, typeof(MainActivity));
            PendingIntent pendingIntent = PendingIntent.GetActivity(Android.App.Application.Context, 0, intent, 0);
            b.SetContentIntent(pendingIntent);

            notificationManager.Notify(notification_id, b.Build());
        }

        public void ClearNotifications()
        {
            if (notificationManager != null && channelCreated)
            {
                notificationManager.Cancel(notification_id);
            }
        }

        public void ToastNotify(string message)
        {
            var toast = Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long);

            ViewGroup vg = (ViewGroup)toast.View;       // To change toast text size/ etc, must grab underlying textview
            TextView tv = (TextView) vg.GetChildAt(0);
            tv.SetTextSize(Android.Util.ComplexUnitType.Sp,25);
            
            toast.Show();
        }

    }
}