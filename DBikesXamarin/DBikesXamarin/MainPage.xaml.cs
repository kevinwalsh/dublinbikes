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
            GetStations();
        }

        public async void GetStations()
        {
            stations = await DBikesHttpHelper.GetAllStations();
            StationsListView.ItemsSource = stations;
        }

        public async void popup(string result = "blank message")
        {
            await DisplayAlert("Welcome!", result, "Continue");
        }

    }

}
