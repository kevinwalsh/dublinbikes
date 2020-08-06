using DBikesXamarin.Models;
using Xamarin.Forms;

namespace DBikesXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            bool isProduction = false;
            DBikesSettings.SetAppSettings(isProduction);
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
