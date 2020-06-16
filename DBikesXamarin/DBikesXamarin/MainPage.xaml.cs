using DBikesXamarin.Helpers;
using DBikesXamarin.Helpers.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DBikesXamarin
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        List<BikeStation> stations;
        Timer mytimer;
        int repeatIntervalMilliseconds = 15000;

        public MainPage()
        {
            InitializeComponent();
            GetAllStations();
            CreateTimer();
        }

        protected override void OnDisappearing()        // when app closed
        {
            mytimer.Stop();
            DependencyService.Get<INotification>().ClearNotifications();
        }

        public async void GetAllStations()
        {
            stations = await DBikesHttpHelper.GetAllStations();
            DisplayStations(stations);
        }

        public async void GetNearbyStations(int stationId)
        {
            stations = await DBikesHttpHelper.GetNearbyStations(stationId);
            DisplayStations(stations);
        }

        public async void GetSingleStation(int stationId)
        {
            stations = await DBikesHttpHelper.GetStation(stationId);
            DisplayStations(stations);
            if(selectedStation != null & stations != null)
            {
                SetSelectedStation(stations.First());
            }
        }

        public void SetSelectedStation(BikeStation bs)
        {
            var oldStationData = selectedStation.BindingContext as BikeStation;
            CheckForChanges(oldStationData, bs);
            selectedStation.BindingContext = bs;
        }

        public void CheckForChanges(BikeStation old, BikeStation stn)
        {
            if (old == null || stn == null || old.stationNumber != stn.stationNumber) { 
                return;                 // ignore changes where station cleared, initially set, or target station changed
            }

            // if (old.available != stn.available || old.free != stn.free)       // simple "if changed"
            var isPriority = false;
            if ((stn.available <= 3 && stn.available < old.available)
                || (stn.free <= 3 && stn.free < old.free))           // If full or almost-full, AND spots have been taken
            {
                isPriority = true;
                //Vibration.Vibrate(200);
            }
            MakeNotification(stn,isPriority);

        }

        public async void DisplayStations(List<BikeStation> stations)
        {
            if (stations == null)
            {
                await DisplayAlert("ERROR", "Error retrieving stations list from server", "OK");
            }
             StationsListView.ItemsSource = stations;
        }

        #region timer

         void CreateTimer()
        {
            mytimer = new Timer();
            mytimer.Interval = repeatIntervalMilliseconds;
            mytimer.AutoReset = true;
            mytimer.Elapsed += OnTimedEvent;
        }

        void StartTimer()
        {
            mytimer.Start();
        }

        private void StopTimer()
        {
            mytimer.Stop();
        }

        private async void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            var stn = selectedStation.BindingContext as BikeStation;
        
            Device.BeginInvokeOnMainThread(() => {
                GetSingleStation(stn.stationNumber);
            });
        }

        #endregion

        #region UI_Messages
        public async void MakeDialogPopup(string result = "blank message")
        {
            await DisplayAlert("Welcome!", result, "Continue");
        }

        public void MakeNotification(BikeStation bs, bool isPriority)
        {
            var title = bs.stationName;
            var msg = bs.available + " bikes, " + bs.free + " stations remaining";
            DependencyService.Get<INotification>().Notify(title, msg, isPriority) ;
        }

        public void MakeToastNotification(string msg)
        {
            DependencyService.Get<INotification>().ToastNotify(msg);
        }

        #endregion

        #region ButtonEvents
        private void SearchAllButton_Clicked(object sender, System.EventArgs e)
        {
            GetAllStations();
        }

        private void StationsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            BikeStation stn2 = (BikeStation) e.Item;    //test
        }

        private void StationsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)  
                                        // similar to itemtapped; but doesnt fire if this item was previously selected
        {
            BikeStation stn2 = (BikeStation)e.SelectedItem; //test
        }

        private void Menu_FindNearby_Clicked(object sender, System.EventArgs e)
        {
            var menuitem = (MenuItem)sender;
            var station = (BikeStation) menuitem.CommandParameter;
            GetNearbyStations(station.stationNumber);
        }
        private void Menu_WatchStation_Clicked(object sender, System.EventArgs e)
        {
            var menuitem = (MenuItem)sender;
            var station = (BikeStation)menuitem.CommandParameter;
            GetSingleStation(station.stationNumber);
            MakeNotification(station, true);
            StartTimer();
        }
        private void ClearWatcher_Clicked(object sender, System.EventArgs e)
        {
            SetSelectedStation(null);
            Vibration.Vibrate(200);
            MakeToastNotification("Station Watcher Cleared!");
            StopTimer();

        }
        #endregion


    }

}
