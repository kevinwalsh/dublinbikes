using DBikesXamarin.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace DBikesXamarin
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        List<BikeStation> stations;
        
        public MainPage()
        {
            InitializeComponent();
            GetAllStations();
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

        public void SetSelectedStation(BikeStation bs)
        {
            selectedStation.BindingContext = bs;
        }

        public async void DisplayStations(List<BikeStation> stations)
        {
            if (stations == null)
            {
                await DisplayAlert("ERROR", "Error retrieving stations list from server", "OK");
            }
             StationsListView.ItemsSource = stations;
        }

        public async void popup(string result = "blank message")
        {
            await DisplayAlert("Welcome!", result, "Continue");
        }

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
            SetSelectedStation(station);
            popup("TODO: Set listener to update this station");
        }
        private void ClearWatcher_Clicked(object sender, System.EventArgs e)
        {
            SetSelectedStation(null);
        }
        #endregion


    }

}
